using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace tweets
{
    public class UserTweetManager
    {
        public static IEnumerable<User> XMLReader()
        {
            Func<string, IEnumerable<User>> creator = url =>
            {
                List<User> users = new List<User>();

                foreach (XElement userNode in XDocument.Load(url).Descendants("User"))
                {
                    User user = new User();
                    user.UserName = userNode.Element("UserName").Value;
                    user.UserEmail = userNode.Element("UserEmail").Value;
                    user.Tweets = new List<Tweet>();
                    foreach (XElement tweetNode in userNode.Element("Tweets").Descendants("Tweet"))
                    {
                        Tweet tweet = new Tweet();
                        tweet.Content = tweetNode.Element("Content").Value;
                        tweet.Flagged = bool.Parse(tweetNode.Element("Flagged").Value);
                        tweet.Year = int.Parse(tweetNode.Element("Year").Value);
                        user.Tweets.Add(tweet);
                    }
                    users.Add(user);
                }
                return users;
            };

            return creator("https://users.nik.uni-obuda.hu/siposm/db/user-tweets.xml");
        }


        // !!!
        // returning 'object' is ONLY NOW ACCEPTABLE, otherwise should use explicitely made classes!!!
        // !!!
        public static IEnumerable<object> GetUsersWithTweetCount(UserTweetContext db)
        {
            var q = from x in db.Users
                    select new
                    {
                        UserName = x.UserName,
                        TweetCount = x.Tweets.Count()
                    };

            return q;
        }

        public static IEnumerable<object> AverageTweetLengthByFlag(UserTweetContext db)
        {
            var q = from x in db.Tweets
                    group x by x.Flagged into g
                    select new
                    {
                        IsFlagged = g.Key,
                        AverageLength = g.Average(a => a.Content.Length)
                    };

            return q;
        }

        public static IEnumerable<object> SumOfTweetYearsByFlag(UserTweetContext db)
        {
            var q = from x in db.Tweets
                    group x by x.Flagged into g
                    select new
                    {
                        IsFlagged = g.Key,
                        SumYears = g.Sum(s => s.Year)
                    };

            return q;
        }

        public static IEnumerable<object> GetTweetNumberForMailType(UserTweetContext db)
        {
            // tolist needed unfortunately...
            // or alternatively: group x by x.UserEmail.Substring(x.UserEmail.IndexOf('@')) into g

            var q = from x in db.Users.ToList()
                    group x by x.UserEmail.Split('@')[1] into g                    
                    select new
                    {
                        MailType = g.Key,
                        TweetCount = g.Sum(c => c.Tweets.Count()),
                    };

            return q;
        }
    }
}
