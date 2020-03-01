using System;
using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class CombineKeyComparer<TSource, TKey> : IComparer<TSource>
    {
        public CombineKeyComparer(Func<TSource, TKey> keySelector, IComparer<TKey> keyComparer)
        {
            KeySelector = keySelector;
            KeyComparer = keyComparer;
        }

        private Func<TSource, TKey> KeySelector { get; }
        private IComparer<TKey> KeyComparer { get; }

        public int Compare(TSource element, TSource minElement)
        {
            return KeyComparer.Compare(KeySelector(element), KeySelector(minElement));
        }
    }
}