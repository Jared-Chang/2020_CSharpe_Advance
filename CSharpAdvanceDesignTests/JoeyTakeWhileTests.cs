﻿using System.Collections.Generic;
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

            var actual = JoeyTakeWhile(cards);

            var expected = new List<Card>
            {
                new Card {Kind = CardKind.Normal, Point = 2},
                new Card {Kind = CardKind.Normal, Point = 3},
                new Card {Kind = CardKind.Normal, Point = 4}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Card> JoeyTakeWhile(IEnumerable<Card> cards)
        {
            using var enumerator = cards.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (current.Kind != CardKind.Normal)
                {
                    yield break;
                }

                yield return current;
            }
        }
    }
}