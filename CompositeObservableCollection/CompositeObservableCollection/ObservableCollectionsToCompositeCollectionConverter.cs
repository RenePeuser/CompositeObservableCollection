using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace CompositeObservableCollection
{
    public class ObservableCollectionsToCompositeCollectionConverter : MarkupExtension, IMultiValueConverter
    {
        private static readonly ObservableCollectionsToCompositeCollectionConverter Instance = new ObservableCollectionsToCompositeCollectionConverter();
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Instance;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var observableCollections = values.OfType<INotifyCollectionChanged>().ToArray();
            return new CompositionObservableCollection(observableCollections);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
