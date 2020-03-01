﻿using System;
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyJoinTests
    {
        [Test]
        public void all_pets_and_owner()
        {
            var david = new Employee {FirstName = "David", LastName = "Chen"};
            var joey = new Employee {FirstName = "Joey", LastName = "Chen"};
            var tom = new Employee {FirstName = "Tom", LastName = "Chen"};

            var employees = new[]
            {
                david,
                joey,
                tom
            };

            var pets = new[]
            {
                new Pet {Name = "Lala", Owner = joey},
                new Pet {Name = "Didi", Owner = david},
                new Pet {Name = "Fufu", Owner = tom},
                new Pet {Name = "QQ", Owner = joey}
            };

            var actual = JoeyJoin(employees, pets, employee => employee, arg => arg.Owner,
                (employee1, pet) => Tuple.Create(employee1.FirstName, pet.Name));

            var expected = new[]
            {
                Tuple.Create("David", "Didi"),
                Tuple.Create("Joey", "Lala"),
                Tuple.Create("Joey", "QQ"),
                Tuple.Create("Tom", "Fufu")
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Tuple<string, string>> JoeyJoin(
            IEnumerable<Employee> employees,
            IEnumerable<Pet> pets,
            Func<Employee, Employee> outterKeySelector,
            Func<Pet, Employee> innerKeySelector,
            Func<Employee, Pet, Tuple<string, string>> resultSelector)
        {
            foreach (var employee in employees)
            {
                var equalityComparer = EqualityComparer<Employee>.Default;
                foreach (var pet in pets.Where(p =>
                    equalityComparer.Equals(outterKeySelector(employee), innerKeySelector(p))))
                {
                    yield return resultSelector(employee, pet);
                }
            }
        }
    }
}