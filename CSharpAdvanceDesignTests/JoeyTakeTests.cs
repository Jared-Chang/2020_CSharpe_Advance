using System.Collections.Generic;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyTakeTests
    {
        [Test]
        public void take_2_employees()
        {
            var employees = GetEmployees();

            var actual = JoeyTake(employees, 2);

            var expected = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void take_3_employees()
        {
            var employees = GetEmployees();

            var actual = JoeyTake(employees, 3);

            var expected = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void take_4_names()
        {
            var names = new[] {"Tom", "Joey", "David"};

            var actual = JoeyTake(names, 4);

            var expected = new[] {"Tom", "Joey", "David"};

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private static IEnumerable<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Mike", LastName = "Chang"},
                new Employee {FirstName = "Joseph", LastName = "Yao"}
            };
        }

        private IEnumerable<TSource> JoeyTake<TSource>(IEnumerable<TSource> source, int count)
        {
            var index = 0;
            using var enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (index < count)
                {
                    yield return enumerator.Current;
                }
                else
                {
                    yield break;
                }

                index++;
            }
        }
    }
}