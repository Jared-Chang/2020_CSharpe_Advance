using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lab.Entities;

namespace Lab
{
    public interface IMyOrderedEnumerable : IEnumerable<Employee>
    {
        IMyOrderedEnumerable Append(IComparer<Employee> currentComparer);
    }

    public class MyOrderedEnumerable : IMyOrderedEnumerable
    {
        private readonly IEnumerable<Employee> _source;
        private IComparer<Employee> _untilNowComparer;

        public MyOrderedEnumerable(IEnumerable<Employee> source, IComparer<Employee> untilNowComparer)
        {
            _source = source;
            _untilNowComparer = untilNowComparer;
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            return JoeySort(_source, _untilNowComparer);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IMyOrderedEnumerable Append(IComparer<Employee> currentComparer)
        {
            _untilNowComparer = new ComboComparer(_untilNowComparer, currentComparer);
            return this;
        }

        public IEnumerator<Employee> JoeySort(IEnumerable<Employee> employees,
            IComparer<Employee> comboComparer)
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