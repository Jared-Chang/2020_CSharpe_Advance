using System;
using System.Collections.Generic;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyTakeWhileTests
    {
        [Test]
        public void take_cards_until_separate_card()
        {
            var cards = new List<Card>
            {
                new Card {Kind = CardKind.Normal, Point = 2},
                new Card {Kind = CardKind.Normal, Point = 3},
                new Card {Kind = CardKind.Normal, Point = 4},
                new Card {Kind = CardKind.Separate},
                new Card {Kind = CardKind.Normal, Point = 5},
                new Card {Kind = CardKind.Normal, Point = 6}
            };

            var actual = JoeyTakeWhile(cards, current => current.Kind == CardKind.Normal);

            var expected = new List<Card>
            {
                new Card {Kind = CardKind.Normal, Point = 2},
                new Card {Kind = CardKind.Normal, Point = 3},
                new Card {Kind = CardKind.Normal, Point = 4}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void take_cards_until_normal_card()
        {
            var cards = new List<Card>
            {
                new Card {Kind = CardKind.Normal, Point = 2},
                new Card {Kind = CardKind.Normal, Point = 3},
                new Card {Kind = CardKind.Normal, Point = 4},
                new Card {Kind = CardKind.Separate},
                new Card {Kind = CardKind.Normal, Point = 5},
                new Card {Kind = CardKind.Normal, Point = 6}
            };

            var actual = JoeyTakeWhile(cards, current => current.Kind == CardKind.Separate);

            var expected = new List<Card>();
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Card> JoeyTakeWhile(IEnumerable<Card> cards, Func<Card, bool> predicate)
        {
            using var enumerator = cards.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (predicate(current))
                {
                    yield return current;
                    continue;
                }

                yield break;
            }
        }
    }
}