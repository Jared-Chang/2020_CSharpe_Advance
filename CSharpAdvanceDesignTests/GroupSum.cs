using System;
using System.Collections.Generic;
using ExpectedObjects;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class GroupSumTests
    {
        [Test]
        public void group_sum_of_3_saving()
        {
            var accounts = new[]
            {
                new Account {Name = "Joey", Saving = 10},
                new Account {Name = "David", Saving = 20},
                new Account {Name = "Tom", Saving = 30},
                new Account {Name = "Joseph", Saving = 40},
                new Account {Name = "Jackson", Saving = 50},
                new Account {Name = "Terry", Saving = 60},
                new Account {Name = "Mary", Saving = 70},
                new Account {Name = "Peter", Saving = 80},
                new Account {Name = "Jerry", Saving = 90},
                new Account {Name = "Martin", Saving = 100},
                new Account {Name = "Bruce", Saving = 110}
            };

            //sum of all Saving of each group which 3 Account per group
            var actual = JoeyGroupSum(accounts, index => index / 3, (sum, account) => sum + account.Saving, 0);

            var expected = new[] {60, 150, 240, 210};

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void group_sum_of_4_saving()
        {
            var accounts = new[]
            {
                new Account {Name = "Joey", Saving = 10},
                new Account {Name = "David", Saving = 20},
                new Account {Name = "Tom", Saving = 30},
                new Account {Name = "Joseph", Saving = 40},
                new Account {Name = "Jackson", Saving = 50},
                new Account {Name = "Terry", Saving = 60},
                new Account {Name = "Mary", Saving = 70},
                new Account {Name = "Peter", Saving = 80},
                new Account {Name = "Jerry", Saving = 90},
                new Account {Name = "Martin", Saving = 100},
                new Account {Name = "Bruce", Saving = 110}
            };

            var actual = JoeyGroupSum(accounts, index => index / 4, (sum, account) => sum + account.Saving, 0);

            var expected = new[] {100, 260, 300};

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void group_sum_of_4_savin_and_devide_by_10()
        {
            var accounts = new[]
            {
                new Account {Name = "Joey", Saving = 10},
                new Account {Name = "David", Saving = 20},
                new Account {Name = "Tom", Saving = 30},
                new Account {Name = "Joseph", Saving = 40},
                new Account {Name = "Jackson", Saving = 50},
                new Account {Name = "Terry", Saving = 60},
                new Account {Name = "Mary", Saving = 70},
                new Account {Name = "Peter", Saving = 80},
                new Account {Name = "Jerry", Saving = 90},
                new Account {Name = "Martin", Saving = 100},
                new Account {Name = "Bruce", Saving = 110}
            };

            var actual = JoeyGroupSum(accounts, index => index / 4, (sum, account) => sum + account.Saving / 10, 0);

            var expected = new[] {10, 26, 30};

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void group_sum_of_4_saving_default_by_10()
        {
            var accounts = new[]
            {
                new Account {Name = "Joey", Saving = 10},
                new Account {Name = "David", Saving = 20},
                new Account {Name = "Tom", Saving = 30},
                new Account {Name = "Joseph", Saving = 40},
                new Account {Name = "Jackson", Saving = 50},
                new Account {Name = "Terry", Saving = 60},
                new Account {Name = "Mary", Saving = 70},
                new Account {Name = "Peter", Saving = 80},
                new Account {Name = "Jerry", Saving = 90},
                new Account {Name = "Martin", Saving = 100},
                new Account {Name = "Bruce", Saving = 110}
            };

            var actual =
                JoeyGroupSum(accounts, index => index / 4, (sum, account) => sum + account.Saving, 10);

            var expected = new[] {110, 270, 310};

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void concatenate_string_of_group()
        {
            var strings = new[]
            {
                "1", "2", "3", "4", "5", "6", "7"
            };

            var actual = JoeyGroupSum(strings, index => index / 3, (sum, aString) => sum + aString, "");

            var expected = new[] {"123", "456", "7"};

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TResult> JoeyGroupSum<TSource, TResult>(IEnumerable<TSource> sources,
            Func<int, int> generateKey,
            Func<TResult, TSource, TResult> accumulator, TResult defaultValue)
        {
            var groups = new Dictionary<int, List<TSource>>();

            var index = 0;

            foreach (var source in sources)
            {
                var groupKey = generateKey(index);
                if (!groups.ContainsKey(groupKey))
                {
                    groups[groupKey] = new List<TSource>();
                }

                groups[groupKey].Add(source);

                index++;
            }

            var sums = new List<TResult>();

            foreach (var group in groups)
            {
                var sum = defaultValue;

                foreach (var account in group.Value)
                {
                    sum = accumulator(sum, account);
                }

                sums.Add(sum);
            }

            return sums;
        }
    }

    public class Account
    {
        public int Saving { get; set; }
        public string Name { get; set; }
    }
}