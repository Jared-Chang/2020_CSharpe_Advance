using System;
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

            var actual = JoeyJoin(
                employees,
                pets,
                employee => employee,
                pet => pet.Owner,
                (employee, pet) => Tuple.Create(employee.FirstName, pet.Name),
                EqualityComparer<Employee>.Default);

            var expected = new[]
            {
                Tuple.Create("David", "Didi"),
                Tuple.Create("Joey", "Lala"),
                Tuple.Create("Joey", "QQ"),
                Tuple.Create("Tom", "Fufu")
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TResult> JoeyJoin<TOutter, TInner, TKey, TResult>(
            IEnumerable<TOutter> employees,
            IEnumerable<TInner> pets,
            Func<TOutter, TKey> outterKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOutter, TInner, TResult> resultSelector,
            IEqualityComparer<TKey> equalityComparer)
        {
            foreach (var employee in employees)
            {
                foreach (var pet in pets.Where(p =>
                    equalityComparer.Equals(outterKeySelector(employee), innerKeySelector(p))))
                {
                    yield return resultSelector(employee, pet);
                }
            }
        }
    }
}