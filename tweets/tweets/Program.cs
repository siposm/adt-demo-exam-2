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

            UserTweetManager.GetUsersWithHotmailAccount(db).ToConsole("Q1");
            UserTweetManager.GetUsersWithAtLeastOneOldTweet(db).ToConsole("Q2"); ;
            UserTweetManager.GetUsersWithTweetCount(db).ToConsole("Q3");;
            UserTweetManager.AverageTweetLengthByFlag(db).ToConsole("Q4");;
            UserTweetManager.SumOfTweetYearsByFlag(db).ToConsole("Q5");;
            UserTweetManager.GetTweetNumberForMailType(db).ToConsole("Q6");;

        }
    }
}
