using System;
using System.Collections.Generic;
using ExpectedObjects;
using Lab;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyLastOrDefaultTests
    {
        [Test]
        public void get_null_when_employees_is_empty()
        {
            var employees = new List<Employee>();
            var actual = employees.JoeyLastOrDefault(item => true);
            Assert.IsNull(actual);
        }

        [Test]
        public void get_last_chang()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Cash", LastName = "Li"}
            };

            var actual = employees.JoeyLastOrDefault(current => current.LastName == "Chang");

            Assert.IsNull(actual);
        }


        [Test]
        public void get_last_li()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Cash", LastName = "Li"}
            };

            var employee = employees.JoeyLastOrDefault(item => item.LastName == "Li");

            new Employee {FirstName = "Cash", LastName = "Li"}
                .ToExpectedObject().ShouldMatch(employee);
        }
    }
}