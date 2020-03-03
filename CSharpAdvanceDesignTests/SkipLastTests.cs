using System.Collections.Generic;
using ExpectedObjects;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class SkipLastTests
    {
        private IEnumerable<int> JoeySkipLast(IEnumerable<int> numbers, int count)
        {
            var queue = new Queue<int>();

            using var enumerator = numbers.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (queue.Count == count)
                {
                    do
                    {
                        yield return queue.Dequeue();
                        queue.Enqueue(enumerator.Current);
                    } while (enumerator.MoveNext());
                }
                else
                {
                    queue.Enqueue(enumerator.Current);
                }
            }
        }

        [Test]
        public void skip_last_2()
        {
            var numbers = new[] {10, 20, 30, 40, 50};
            var actual = JoeySkipLast(numbers, 2);

            var expected = new[] {10, 20, 30};

            expected.ToExpectedObject().ShouldMatch(actual);
        }
    }
}