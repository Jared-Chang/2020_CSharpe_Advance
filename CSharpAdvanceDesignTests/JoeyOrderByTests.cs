using System;
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    public class CombindKeyComparer : IComparer<Employee>
    {
        public CombindKeyComparer(Func<Employee, string> keySelector, IComparer<string> keyComparer)
        {
            KeySelector = keySelector;
            KeyComparer = keyComparer;
        }

        private Func<Employee, string> KeySelector { get; }
        private IComparer<string> KeyComparer { get; }

        public int Compare(Employee employee, Employee minElement) 
        {
            return KeyComparer.Compare(KeySelector(employee), KeySelector(minElement));
        }
    }

    [TestFixture]
    public class JoeyOrderByTests
    {
        //[Test]
        //public void orderBy_lastName()
        //{
        //    var employees = new[]
        //    {
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"}
        //    };

        //    var actual = JoeyOrderByLastNameAndFirstName(employees);

        //    var expected = new[]
        //    {
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joey", LastName = "Wang"}
        //    };

        //    expected.ToExpectedObject().ShouldMatch(actual);
        //}

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

            var actual =
                JoeyOrderByLastNameAndFirstName(employees, new CombindKeyComparer(employee => employee.LastName, Comparer<string>.Default), employee => employee.FirstName, Comparer<string>.Default);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"}
            };


            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyOrderByLastNameAndFirstName(
            IEnumerable<Employee> employees, 
            IComparer<Employee> combindKeyComparer,
            Func<Employee, string> secondKeySelector,
            IComparer<string> secondKeyComparer)
        {
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (var i = 1; i < elements.Count; i++)
                {
                    var employee = elements[i];

                    var firstCompareResult = combindKeyComparer.Compare(employee, minElement);
                    var secondCompareResult =
                        secondKeyComparer.Compare(secondKeySelector(employee), secondKeySelector(minElement));

                    if (firstCompareResult < 0 || firstCompareResult == 0 && secondCompareResult < 0)
                    {
                        minElement = employee;
                        index = i;
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }
    }
}