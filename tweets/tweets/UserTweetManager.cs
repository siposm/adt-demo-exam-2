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
    }
}
