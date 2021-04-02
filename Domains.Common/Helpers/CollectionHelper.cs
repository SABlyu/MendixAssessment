using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Common.Helpers
{
    public static class CollectionHelper
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
                action(item);
        }
    }
}
