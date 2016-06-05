using System.Collections.Generic;
using System.Linq;

namespace CompositeObservableCollection
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> itemsToAdd)
        {
            itemsToAdd.ToList().ForEach(source.Add);
        }

        public static void RemoveRange<T>(this ICollection<T> source, IEnumerable<T> itemsToRemove)
        {
            itemsToRemove.ToList().ForEach(item => source.Remove(item));
        }
    }
}
