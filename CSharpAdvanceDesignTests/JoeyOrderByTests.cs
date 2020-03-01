using System.Collections.Generic;
using System.Linq;
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

            var actual = employees.JoeySort(new CombineKeyComparer<string>(element => element.LastName, Comparer<string>.Default));

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

            var comboComparer = new ComboComparer(
                new CombineKeyComparer<string>(element => element.LastName, Comparer<string>.Default),
                new CombineKeyComparer<string>(employee => employee.FirstName, Comparer<string>.Default));

            var actual = employees.JoeySort(comboComparer);

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

            var comboComparer = new ComboComparer(
                new ComboComparer(
                    new CombineKeyComparer<string>(element => element.LastName, Comparer<string>.Default),
                    new CombineKeyComparer<string>(employee => employee.FirstName, Comparer<string>.Default)),
                new CombineKeyComparer<int>(element => element.Age, Comparer<int>.Default));

            var actual = employees.JoeySort(comboComparer);

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
    }
}