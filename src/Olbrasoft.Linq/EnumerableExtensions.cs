using System;
using System.Collections.Generic;
using System.Linq;

namespace Olbrasoft.Linq
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> SplitToEnumerableOfEnumerable<T>(this IEnumerable<T> enumerable, int maxListSize = 7000)
        {
            var result = enumerable.ToList();
            for (var i = 0; i < result.Count; i += maxListSize)
            {
                yield return result.GetRange(i, Math.Min(maxListSize, result.Count - i));
            }
        }

        public static Tuple<IEnumerable<T>, IEnumerable<T>> SplitToTwo<T>(this IEnumerable<T> enumerable)
        {
            var count = enumerable.Count();

            var maxSize = count / 2;

            if (count % 2 > 0) maxSize++;

            var enumerables = enumerable.SplitToEnumerableOfEnumerable(maxSize);

            return new Tuple<IEnumerable<T>, IEnumerable<T>>(enumerables.First(), enumerables.Last());
        }
    }
}