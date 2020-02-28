using System;
using System.Collections.Generic;

namespace Lab
{
    static public class LinqExtensions
    {
        public static List<TSource> JoeyWhere<TSource>(this List<TSource> source, Predicate<TSource> predicate)
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
    }
}