using System.Collections.Generic;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySkipTests
    {
        [Test]
        public void skip_2_employees()
        {
            var employees = (IEnumerable<Employee>) new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Mike", LastName = "Chang"},
                new Employee {FirstName = "Joseph", LastName = "Yao"}
            };

            var actual = JoeySkip(employees, 2);

            var expected = new List<Employee>
            {
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Mike", LastName = "Chang"},
                new Employee {FirstName = "Joseph", LastName = "Yao"}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }


        [Test]
        public void skip_3_numberStrins()
        {
            var numberStrins = new[]
            {
                "1",
                "2",
                "3",
                "4",
                "5"
            };

            var actual = JoeySkip(numberStrins, 3);

            var expected = new[]
            {
                "4",
                "5"
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TSource> JoeySkip<TSource>(IEnumerable<TSource> source, int count)
        {
            using var enumerator = source.GetEnumerator();

            var index = 0;

            while (enumerator.MoveNext())
            {
                if (index < count)
                {
                    index++;
                    continue;
                }

                yield return enumerator.Current;
            }
        }
    }
}