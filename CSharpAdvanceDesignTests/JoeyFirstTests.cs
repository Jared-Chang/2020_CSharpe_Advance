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

        [Test]
        public void girl_is_empty_should_throw_exception()
        {
            var girls = new Girl[]
            {
            };

            TestDelegate action = () => JoeyFirst(girls, current => current.Age > 25);

            Assert.Throws<InvalidOperationException>(action);
        }

        [Test]
        public void first_number_GT_10()
        {
            var numbers = new[]
            {
                1, 2, 50, 60, 5
            };

            Func<int, bool> predicate = current => current > 10;
            var girl = JoeyFirst(numbers, predicate);
            var expected = 50;

            expected.ToExpectedObject().ShouldMatch(girl);
        }

        private TSource JoeyFirst<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (predicate(current))
                {
                    return current;
                }
            }

            throw new InvalidOperationException($"{nameof(source)} has no expected result");
        }
    }
}