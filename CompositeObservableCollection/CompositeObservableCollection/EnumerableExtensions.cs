using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CompositeObservableCollection
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            source.ToList().ForEach(action);
        }

        public static List<T> ToListOfType<T>(this IEnumerable source)
        {
            return source?.OfType<T>().ToList() ?? new List<T>();
        }
    }
}
