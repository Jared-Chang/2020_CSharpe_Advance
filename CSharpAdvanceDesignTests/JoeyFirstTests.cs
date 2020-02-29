using System;
using System.Collections.Generic;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyFirstTests
    {
        [Test]
        public void get_first_girl()
        {
            var girls = new[]
            {
                new Girl {Age = 10},
                new Girl {Age = 20},
                new Girl {Age = 30}
            };

            var girl = JoeyFirst(girls, girl1 => { return true; });
            var expected = new Girl {Age = 10};

            expected.ToExpectedObject().ShouldMatch(girl);
        }

        [Test]
        public void get_first_girl_age_GT_25()
        {
            var girls = new[]
            {
                new Girl {Age = 10},
                new Girl {Age = 20},
                new Girl {Age = 30}
            };

            var girl = JoeyFirst(girls, current => current.Age > 25);
            var expected = new Girl {Age = 30};

            expected.ToExpectedObject().ShouldMatch(girl);
        }

        private Girl JoeyFirst(IEnumerable<Girl> girls, Func<Girl, bool> predicate)
        {
            using var enumerator = girls.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (predicate(current))
                {
                    return current;
                }
            }

            throw new InvalidOperationException();
        }
    }
}