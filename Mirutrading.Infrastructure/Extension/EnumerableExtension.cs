using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public static class EnumerableExtension
    {
        public static bool HasValue<T>(this IEnumerable<T> obj)
        {
            return obj != null && obj.Any();
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            foreach(T t in @this)
            {
                action(t);
            }
            return @this;
        }
    }
}
