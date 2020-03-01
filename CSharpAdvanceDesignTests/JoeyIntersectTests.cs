using System.Collections.Generic;
using ExpectedObjects;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyIntersectTests
    {
        [Test]
        public void intersect_numbers()
        {
            var first = new[] {1, 3, 5, 3};
            var second = new[] {5, 7, 3, 7};

            var actual = JoeyIntersect(first, second);

            var expected = new[] {3, 5};

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> JoeyIntersect(IEnumerable<int> first, IEnumerable<int> second)
        {
            var secondHashSet = new HashSet<int>(second);

            using var secondEnumerator = first.GetEnumerator();
            while (secondEnumerator.MoveNext())
            {
                var current = secondEnumerator.Current;

                if (secondHashSet.Remove(current))
                {
                    yield return current;
                }
            }
        }
    }
}