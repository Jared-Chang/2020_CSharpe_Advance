using System;
using System.Collections.Generic;

namespace Lab
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            var list = new List<TSource>();

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    list.Add(item);
                }
            }

            return list;
        }

        public static IEnumerable<TReturn> JoeySelect<TSource, TReturn>(this IEnumerable<TSource> source,
            Func<TSource, TReturn> transform)
        {
            var result = new List<TReturn>();

            foreach (var item in source)
            {
                result.Add(transform(item));
            }

            return result;
        }

        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> source,
            Func<TSource, int, bool> predicate)
        {
            var result = new List<TSource>();

            var index = 0;

            foreach (var item in source)
            {
                if (predicate(item, index))
                {
                    result.Add(item);
                }

                index++;
            }

            return result;
        }

        public static IEnumerable<TReturn> JoeySelect<TSource, TReturn>(this IEnumerable<TSource> source,
            Func<TSource, int, TReturn> selector)
        {
            var index = 0;
            var result = new List<TReturn>();

            foreach (var item in source)
            {
                result.Add(selector(item, index));
                index++;
            }

            return result;
        }
    }
}