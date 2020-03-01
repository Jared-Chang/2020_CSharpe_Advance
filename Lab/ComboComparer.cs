using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class ComboComparer : IComparer<Employee>
    {
        public ComboComparer(IComparer<Employee> firstComparer, IComparer<Employee> secondComparer)
        {
            FirstComparer = firstComparer;
            SecondComparer = secondComparer;
        }

        public IComparer<Employee> FirstComparer { get; }
        public IComparer<Employee> SecondComparer { get; }

        public int Compare(Employee element, Employee minElement)
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