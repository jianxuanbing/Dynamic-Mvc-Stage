using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core.Extensions
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<TSource> FilterType<TSource,  TFilterType>(this IEnumerable<TSource> source , Func<TSource , Type> selector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            foreach (var item in source)
                if (!typeof(TFilterType).IsAssignableFrom(selector(item))) yield return item;
        }
    }
}
