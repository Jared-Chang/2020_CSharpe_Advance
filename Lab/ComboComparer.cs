using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class ComboComparer<TSource> : IComparer<TSource>
    {
        public ComboComparer(IComparer<TSource> firstComparer, IComparer<TSource> secondComparer)
        {
            FirstComparer = firstComparer;
            SecondComparer = secondComparer;
        }

        public IComparer<TSource> FirstComparer { get; }
        public IComparer<TSource> SecondComparer { get; }

        public int Compare(TSource element, TSource minElement)
        {
            var firstCompareResult = FirstComparer.Compare(element, minElement);

            if (firstCompareResult != 0)
            {
                return firstCompareResult;
            }

            return SecondComparer.Compare(element, minElement);
        }
    }
}