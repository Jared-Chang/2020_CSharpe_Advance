using System.Collections.Generic;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyDistinctTests
    {
        [Test]
        public void distinct_numbers()
        {
            var numbers = new[] {91, 3, 91, -1};
            var actual = Distinct(numbers);

            var expected = new[] {91, 3, -1};

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void distinct_employee()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"}
            };

            var actual = JoeyDistinct(employees);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"}
            };


            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> Distinct(IEnumerable<int> numbers)
        {
            return new HashSet<int>(numbers);
        }

        private IEnumerable<Employee> JoeyDistinct(IEnumerable<Employee> employees)
        {
            return new HashSet<Employee>(employees, new FullNameEqualityComparer());
        }
    }

    internal class FullNameEqualityComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.FirstName == y.FirstName && x.LastName == y.LastName;
        }

        public int GetHashCode(Employee obj)
        {
            return new {obj.FirstName, obj.LastName}.GetHashCode();
        }
    }
}