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

        private bool JoeyAll(IEnumerable<Girl> girls, Func<Girl, bool> predicate)
        {
            foreach (var girl in girls)
            {
                if (!predicate(girl))
                {
                    return false;
                }
            }

            return true;
        }
    }
}