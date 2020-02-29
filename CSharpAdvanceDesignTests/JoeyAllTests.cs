using System.Collections.Generic;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyAllTests
    {
        [Test]
        public void girls_age_greater_than_18()
        {
            var girls = new List<Girl>
            {
                new Girl {Age = 20},
                new Girl {Age = 21},
                new Girl {Age = 17},
                new Girl {Age = 18},
                new Girl {Age = 30}
            };

            var actual = JoeyAll(girls);
            Assert.IsFalse(actual);
        }

        private bool JoeyAll(IEnumerable<Girl> girls)
        {
            foreach (var girl in girls)
            {
                if (!(girl.Age >= 18))
                {
                    return false;
                }
            }

            return true;
        }
    }
}