using System;
using System.Collections.Generic;

namespace Lab
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            using var enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (predicate(enumerator.Current))
                {
                    yield return enumerator.Current;
                }
            }
        }

        public static IEnumerable<TReturn> JoeySelect<TSource, TReturn>(this IEnumerable<TSource> source,
            Func<TSource, TReturn> selector)
        {
            using var enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                yield return selector(enumerator.Current);
            }
        }

        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> source,
            Func<TSource, int, bool> predicate)
        {
            using var enumerator = source.GetEnumerator();

            var index = 0;

            while (enumerator.MoveNext())
            {
                if (predicate(enumerator.Current, index))
                {
                    yield return enumerator.Current;
                }

                index++;
            }
        }

        public static IEnumerable<TReturn> JoeySelect<TSource, TReturn>(this IEnumerable<TSource> source,
            Func<TSource, int, TReturn> selector)
        {
            using var enumerator = source.GetEnumerator();
            var index = 0;

            while (enumerator.MoveNext())
            {
                yield return selector(enumerator.Current, index);
                index++;
            }
        }

        public static IEnumerable<TSource> JoeyTake<TSource>(this IEnumerable<TSource> source, int count)
        {
            var index = 0;
            using var enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (index < count)
                {
                    yield return enumerator.Current;
                }
                else
                {
                    yield break;
                }

                index++;
            }
        }

        public static IEnumerable<TSource> JoeySkip<TSource>(this IEnumerable<TSource> source, int count)
        {
            using var enumerator = source.GetEnumerator();

            var index = 0;

            while (enumerator.MoveNext())
            {
                if (index < count)
                {
                    index++;
                    continue;
                }

                yield return enumerator.Current;
            }
        }

        public static IEnumerable<TSource> JoeyTakeWhile<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            using var enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (predicate(current))
                {
                    yield return current;
                    continue;
                }

                yield break;
            }
        }

        public static IEnumerable<TResult> JoeyGroupSum<TSource, TResult>(this IEnumerable<TSource> sources,
            Func<int, int> generateKey,
            Func<TResult, TSource, TResult> accumulator, TResult defaultValue)
        {
            var groups = new Dictionary<int, List<TSource>>();

            var index = 0;

            foreach (var source in sources)
            {
                var groupKey = generateKey(index);
                if (!groups.ContainsKey(groupKey))
                {
                    groups[groupKey] = new List<TSource>();
                }

                groups[groupKey].Add(source);

                index++;
            }


            foreach (var group in groups)
            {
                var sum = defaultValue;

                foreach (var account in group.Value)
                {
                    sum = accumulator(sum, account);
                }

                yield return sum;
            }
        }
    }
}