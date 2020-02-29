using System;
using System.Collections.Generic;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySingleTests
    {
        [Test]
        public void no_girls()
        {
            var girls = new Girl[] { };
            TestDelegate action = () => JoeySingle(girls);
            Assert.Throws<InvalidOperationException>(action);
        }

        [Test]
        public void only_one_girl()
        {
            var girls = new[]
            {
                new Girl {Name = "May"}
            };
            var actual = JoeySingle(girls);
            new Girl {Name = "May"}.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void more_than_one_girl()
        {
            var girls = new[]
            {
                new Girl {Name = "May"},
                new Girl {Name = "Jessica"}
            };
            TestDelegate action = () => JoeySingle(girls);
            Assert.Throws<InvalidOperationException>(action);
        }

        private Girl JoeySingle(IEnumerable<Girl> girls)
        {
            using var enumerator = girls.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException($"{nameof(girls)} is empty");
            }

            var result = enumerator.Current;

            if (enumerator.MoveNext())
            {
                throw new InvalidOperationException($"{nameof(girls)} is not single");
            }

            return result;
        }
    }
}