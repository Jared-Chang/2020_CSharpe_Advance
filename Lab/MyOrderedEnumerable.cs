using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lab
{
    public interface IMyOrderedEnumerable<TSource> : IEnumerable<TSource>
    {
        IMyOrderedEnumerable<TSource> Append(IComparer<TSource> currentComparer);
    }

    public class MyOrderedEnumerable<TSource> : IMyOrderedEnumerable<TSource>
    {
        private readonly IEnumerable<TSource> _source;
        private IComparer<TSource> _untilNowComparer;

        public MyOrderedEnumerable(IEnumerable<TSource> source, IComparer<TSource> untilNowComparer)
        {
            _source = source;
            _untilNowComparer = untilNowComparer;
        }

        public IEnumerator<TSource> GetEnumerator()
        {
            return JoeySort(_source, _untilNowComparer);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IMyOrderedEnumerable<TSource> Append(IComparer<TSource> currentComparer)
        {
            _untilNowComparer = new ComboComparer<TSource>(_untilNowComparer, currentComparer);
            return this;
        }

        public IEnumerator<TSource> JoeySort(IEnumerable<TSource> employees,
            IComparer<TSource> comboComparer)
        {
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (var i = 1; i < elements.Count; i++)
                {
                    var element = elements[i];

                    if (comboComparer.Compare(element, minElement) < 0)
                    {
                        minElement = element;
                        index = i;
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }
    }
}