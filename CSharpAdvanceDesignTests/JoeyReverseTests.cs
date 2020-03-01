using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyReverseTests
    {
        [Test]
        public void reverse_employees()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Wang"}
            };

            var actual = JoeyReverse(employees);

            var expected = new List<Employee>
            {
                new Employee {FirstName = "David", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Chen"}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyReverse(IEnumerable<Employee> employees)
        {
            var reversed = new Stack<Employee>();
            using var enumerator = employees.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                reversed.Push(current);
            }

            while (reversed.Any())
            {
                yield return reversed.Pop();
            }
        }
    }
}