using ExpectedObjects;
using Lab;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyOrderByTests
    {
        [Test]
        public void orderBy_lastName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"}
            };

            var actual = employees
                .JoeyOrderBy(employee => employee.LastName);

            var expected = new[]
            {
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void orderBy_lastName_and_firstName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"}
            };

            var actual = employees
                .JoeyOrderBy(employee => employee.LastName)
                .JoeyThenBy(employee => employee.FirstName);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"}
            };


            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void orderBy_lastName_and_firstName_and_age()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 10},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 8},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 50},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 40},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 50}
            };

            var actual = employees
                .JoeyOrderBy(employee => employee.LastName)
                .JoeyThenBy(employee => employee.FirstName)
                .JoeyThenBy(employee => employee.Age);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 40},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 50},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 50},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 8},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 10}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void order_by_girl_name_then_age()
        {
            var girls = new[]
            {
                new Girl {Age = 1, Name = "a"},
                new Girl {Age = 3, Name = "a"},
                new Girl {Age = 4, Name = "b"},
                new Girl {Age = 2, Name = "b"}
            };

            var actual = girls
                .JoeyOrderBy(employee => employee.Name)
                .JoeyThenBy(employee => employee.Age);

            var expected = new[]
            {
                new Girl {Age = 1, Name = "a"},
                new Girl {Age = 3, Name = "a"},
                new Girl {Age = 2, Name = "b"},
                new Girl {Age = 4, Name = "b"}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }
    }
}