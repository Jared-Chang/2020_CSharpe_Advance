using System.Collections.Generic;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySequenceEqualTests
    {
        [Test]
        public void compare_two_numbers_equal()
        {
            var first = new List<int> {3, 2, 1};
            var second = new List<int> {3, 2, 1};

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }

        [Test]
        public void compare_two_numbers_not_equal()
        {
            var first = new List<int> {3, 1, 1};
            var second = new List<int> {3, 2, 1};

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_empty_list_is_equal()
        {
            var first = new List<int>();
            var second = new List<int>();

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }

        [Test]
        public void compare_two_different_length_numbers_is_not_equal()
        {
            var first = new List<int> {1, 2, 3};
            var second = new List<int> {1, 2, 3, 4};

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_different_length_numbers_is_not_equal_2()
        {
            var first = new List<int> {1, 2, 3, 4};
            var second = new List<int> {1, 2, 3};

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        private bool JoeySequenceEqual(IEnumerable<int> first, IEnumerable<int> second)
        {
            using var firstEnumerator = first.GetEnumerator();
            using var secondEnumerator = second.GetEnumerator();

            while (firstEnumerator.MoveNext())
            {
                secondEnumerator.MoveNext();

                if (firstEnumerator?.Current != secondEnumerator?.Current)
                {
                    return false;
                }
            }

            return firstEnumerator.MoveNext() == secondEnumerator.MoveNext();
        }
    }
}