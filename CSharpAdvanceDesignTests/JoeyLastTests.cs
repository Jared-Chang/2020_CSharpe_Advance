using System;
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyLastTests
    {
        [Test]
        public void get_last_employee()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Cash", LastName = "Li"}
            };

            var employee = JoeyLast(employees, employee1 => true);

            new Employee {FirstName = "Cash", LastName = "Li"}
                .ToExpectedObject().ShouldMatch(employee);
        }

        [Test]
        public void get_last_chen()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Cash", LastName = "Li"}
            };

            var employee = JoeyLast(employees, current => current.LastName == "Chen");

            new Employee {FirstName = "David", LastName = "Chen"}
                .ToExpectedObject().ShouldMatch(employee);
        }

        [Test]
        public void get_last_employee_when_no_girls()
        {
            var employees = new Employee[]
            {
            };

            TestDelegate action = () => JoeyLast(employees, employee => true);
            Assert.Throws<InvalidOperationException>(action);
        }

        private Employee JoeyLast(IEnumerable<Employee> employees, Func<Employee, bool> predicate)
        {
            using var enumerator = employees.Reverse().GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                if (predicate(current))
                {
                    return current;
                }
            }

            throw new InvalidOperationException($"{nameof(employees)} is no matched result");
        }
    }
}