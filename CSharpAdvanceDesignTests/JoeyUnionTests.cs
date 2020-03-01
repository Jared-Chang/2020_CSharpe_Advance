using System.Collections.Generic;
using ExpectedObjects;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyUnionTests
    {
        [Test]
        public void union_numbers()
        {
            var first = new[] {1, 3, 5};
            var second = new[] {5, 3, 7};

            var actual = JoeyUnion(first, second);
            var expected = new[] {1, 3, 5, 7};

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> JoeyUnion(IEnumerable<int> first, IEnumerable<int> second)
        {
            var hashSet = new HashSet<int>();

            using var firstEnumerator = first.GetEnumerator();
            while (firstEnumerator.MoveNext())
            {
                var current = firstEnumerator.Current;

                if (hashSet.Add(current))
                {
                    yield return current;
                }
            }

            using var secondEnumerator = second.GetEnumerator();
            while (secondEnumerator.MoveNext())
            {
                var current = secondEnumerator.Current;

                if (hashSet.Add(current))
                {
                    yield return current;
                }
            }
        }
    }
}