using System.Collections.Generic;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyAnyTests
    {
        [Test]
        public void three_employees()
        {
            var emptyEmployees = new[]
            {
                new Employee(),
                new Employee(),
                new Employee()
            };

            var actual = JoeyAny(emptyEmployees);
            Assert.IsTrue(actual);
        }

        [Test]
        public void empty_employees()
        {
            var emptyEmployees = new Employee[]
            {
            };

            var actual = JoeyAny(emptyEmployees);
            Assert.IsFalse(actual);
        }

        private bool JoeyAny(IEnumerable<Employee> employees)
        {
            using var enumerator = employees.GetEnumerator();

            return enumerator.MoveNext();
        }
    }
}