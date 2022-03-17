using System;
using System.Collections.Generic;
using System.Linq;

namespace tweets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserTweetContext db = new UserTweetContext();
            
            List<User> users = UserTweetManager.XMLReader().ToList();

            foreach (var user in users)
            {
                if(Validator.UserEmailValidator(nameof(user.UserEmail), user))
                {
                    db.Users.Add(user);
                }
                else
                {
                    Console.WriteLine($"{user} was not fulfilled the requiremenets!");
                }
            }
            db.SaveChanges();

            foreach (var user in db.Users)
            {
                Console.WriteLine(user);
                foreach (var tweet in user.Tweets)
                {
                    Console.WriteLine("   " + tweet);
                }
            }

            UserTweetManager.GetUsersWithTweetCount(db);
            UserTweetManager.AverageTweetLengthByFlag(db);
            UserTweetManager.SumOfTweetYearsByFlag(db);
            UserTweetManager.GetTweetNumberForMailType(db);

        }
    }
}
