using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using tweets;

namespace Testing
{
    [TestFixture]
    public class LinqTests
    {
        private UserTweetContext DB { get; set; }

        [SetUp]
        public void Init()
        {
            this.DB = new UserTweetContext();
            List<User> users = UserTweetManager.XMLReader().ToList();
            
            foreach (var user in users)
                if (Validator.UserEmailValidator(nameof(user.UserEmail), user))
                    this.DB.Users.Add(user);

            DB.SaveChanges();
        }

        [Test]
        public void Test_GetUsersWithHotmailAccount_FromDbReturnsTwo()
        {            
            var result = UserTweetManager.GetUsersWithHotmailAccount(this.DB);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [TestCase(3)]
        public void Test_GetUsersWithAtLeastOneOldTweet_ReturnsWithoutDuplicates(int correctNumber)
        {
            var result = UserTweetManager.GetUsersWithAtLeastOneOldTweet(this.DB);
            Assert.That(result.Count(), Is.EqualTo(correctNumber));
        }
    }
}
