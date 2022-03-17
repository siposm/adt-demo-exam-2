using System;
using System.Collections.Generic;

namespace tweets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserTweetContext db = new UserTweetContext();
            
            User u = new User();
            u.UserName = "test_user";
            u.UserEmail = "test@mail.com";
            u.Tweets = new List<Tweet>();
            u.Tweets.Add(new Tweet()
            {
                Content = "asdasdasd",
                Flagged = true,
                Year = 2022
            });

            db.Users.Add(u);

            db.SaveChanges();

            foreach (var item in db.Users)
            {
                Console.WriteLine(item);
                foreach (var tw in item.Tweets)
                {
                    Console.WriteLine(tw);
                }
            }
        }
    }
}
