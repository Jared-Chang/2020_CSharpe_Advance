using System;
using System.Collections.Generic;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyAllTests
    {
        [Test]
        public void girls_age_GE_18()
        {
            var girls = new List<Girl>
            {
                new Girl {Age = 20},
                new Girl {Age = 21},
                new Girl {Age = 17},
                new Girl {Age = 18},
                new Girl {Age = 30}
            };

            var actual = JoeyAll(girls, girl => girl.Age >= 18);
            Assert.IsFalse(actual);
        }

        [Test]
        public void girls_age_LE_30()
        {
            var girls = new List<Girl>
            {
                new Girl {Age = 20},
                new Girl {Age = 21},
                new Girl {Age = 17},
                new Girl {Age = 18},
                new Girl {Age = 30}
            };

            var actual = JoeyAll(girls, girl => girl.Age <= 30);
            Assert.IsTrue(actual);
        }

        [Test]
        public void all_strings_equal_joey()
        {
            var names = new[]
            {
                "joey",
                "jared",
                "jack"
            };

            Func<string, bool> predicate = name => name == "joey";
            var actual = JoeyAll(names, predicate);
            Assert.IsFalse(actual);
        }

        private bool JoeyAll<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var item in source)
            {
                if (!predicate(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}