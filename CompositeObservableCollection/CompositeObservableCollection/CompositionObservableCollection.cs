using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace CompositeObservableCollection
{
    public class CompositionObservableCollection : ObservableCollection<object>
    {
        private readonly List<INotifyCollectionChanged> _observableCollections;

        public CompositionObservableCollection(params INotifyCollectionChanged[] observableCollections)
        {
            _observableCollections = observableCollections.ToList();

            InitItems();

            AttacheEvents();
        }

        private void InitItems()
        {
            var itemsToAdd = _observableCollections.OfType<IEnumerable<object>>().SelectMany(item => item);
            this.AddRange(itemsToAdd);
        }

        private void AttacheEvents()
        {
            _observableCollections.ForEach(item => item.CollectionChanged += TestCollectionChanged);
        }

        private void TestCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var newItems = e.NewItems.ToListOfType<object>();
            var oldItems = e.OldItems.ToListOfType<object>();
            var notifyCollectionChanged = (INotifyCollectionChanged)sender;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddItems(newItems, notifyCollectionChanged);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.RemoveRange(newItems);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    ReplaceItems(oldItems, newItems);
                    break;
                case NotifyCollectionChangedAction.Move:
                    MoveItems(newItems, e);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Reset();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Reset()
        {
            var itemsToDelete = GetItemsToDelete().ToList();
            this.RemoveRange(itemsToDelete);
        }

        private void MoveItems(IEnumerable<object> newItems, NotifyCollectionChangedEventArgs e)
        {
            var deltaMoveIndex = e.NewStartingIndex - e.OldStartingIndex;
            foreach (var itemToMove in newItems)
            {
                var indexOfItemInTargetCollection = IndexOf(itemToMove);
                var newIndex = indexOfItemInTargetCollection + deltaMoveIndex;
                Move(indexOfItemInTargetCollection, newIndex);
            }
        }

        private void ReplaceItems(IList<object> oldItems, IList<object> newItems)
        {
            for (var i = 0; i < oldItems.Count; i++)
            {
                var currentIndexInTargetCollection = IndexOf(oldItems[i]);
                this[currentIndexInTargetCollection] = newItems[i];
            }
        }

        private void AddItems(IEnumerable<object> newItems, INotifyCollectionChanged sender)
        {
            foreach (var itemToAdd in newItems)
            {
                var indexOfNewItemToAdd = CalculateIndexForInsertNewItem(sender);
                InsertItem(indexOfNewItemToAdd, itemToAdd);
            }
        }

        private IEnumerable<object> GetItemsToDelete()
        {
            var allSourceItems = _observableCollections.OfType<IEnumerable<object>>().SelectMany(item => item);
            var itemsToDelete = this.Except(allSourceItems);
            return itemsToDelete;
        }

        private int CalculateIndexForInsertNewItem(INotifyCollectionChanged sender)
        {
            var firstItem = CalculateFirstIndexOfItemStack(sender);
            var lastItemIndex = firstItem + ((IEnumerable<object>)sender).Count();
            return lastItemIndex - 1;
        }

        private int CalculateFirstIndexOfItemStack(INotifyCollectionChanged sender)
        {
            var indexInStack = _observableCollections.IndexOf(sender);
            if (indexInStack == 0)
            {
                return 0;
            }

            var itemsCount = CalculateItemsCountReverseFromIndex(indexInStack);
            return itemsCount;
        }

        private int CalculateItemsCountReverseFromIndex(int indexInStack)
        {
            var itemsCount = 0;
            for (var i = indexInStack - 1; i >= 0; i--)
            {
                itemsCount += _observableCollections.OfType<IEnumerable<object>>().ElementAt(i).Count();
            }
            return itemsCount;
        }
    }
}
