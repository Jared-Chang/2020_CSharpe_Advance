using System;
using System.Collections.Generic;

namespace Lab
{
    static public class LinqExtensions
    {
        public static List<TSource> JoeyWhere<TSource>(List<TSource> source, Predicate<TSource> predicate)
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
    }
}