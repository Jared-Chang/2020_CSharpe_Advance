using System.Collections.Generic;
using ExpectedObjects;
using Lab;
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

            var actual = cards.JoeyTakeWhile(current => current.Kind == CardKind.Normal);

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

            var actual = cards.JoeyTakeWhile(current => current.Kind == CardKind.Separate);

            var expected = new List<Card>();
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void take_number_until_negtive()
        {
            var numbers = new[] {1, 2, 3, 4, -1, 5, 6};

            var actual = numbers.JoeyTakeWhile(number => number > 0);

            var expected = new[] {1, 2, 3, 4};
            expected.ToExpectedObject().ShouldMatch(actual);
        }
    }
}