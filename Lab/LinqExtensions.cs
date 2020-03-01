using System;
using System.Collections.Generic;
using System.Linq;
using Lab.Entities;

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

        public static bool JoeyAny<TSource>(this IEnumerable<TSource> numbers, Func<TSource, bool> predicate)
        {
            using var enumerator = numbers.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (predicate(current))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool JoeyAll<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var item in source)
            {
                if (!predicate(item))
                {
                    return false;
                }
            }

            return true;
        }

        public static TSource JoeyFirst<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (predicate(current))
                {
                    return current;
                }
            }

            throw new InvalidOperationException($"{nameof(source)} has no expected result");
        }

        public static TSource JoeyLast<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            using var enumerator = source.GetEnumerator();
            var hasMatch = false;
            var result = default(TSource);

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                if (predicate(current))
                {
                    hasMatch = true;
                    result = current;
                }
            }

            return hasMatch ? result : throw new InvalidOperationException($"{nameof(source)} is no matched result");
        }

        public static TSource JoeyLastOrDefault<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            using var enumerator = source.GetEnumerator();
            var result = default(TSource);

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                if (predicate(current))
                {
                    result = current;
                }
            }

            return result;
        }

        public static IEnumerable<Employee> JoeyOrderBy<TKey>(this IEnumerable<Employee> employees,
            Func<Employee, TKey> keySelector)
        {
            return employees;
        }

        public static IEnumerable<Employee> JoeyThenBy<TKey>(this IEnumerable<Employee> employees,
            Func<Employee, TKey> keySelector)
        {
            return employees;
        }
    }
}