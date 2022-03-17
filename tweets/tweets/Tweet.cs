using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tweets
{
    // https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key

    [Table("Tweets")]
    public class Tweet
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public bool Flagged { get; set; }
        public int Year { get; set; }

        //public virtual int UserId { get; set; }
        public virtual User User { get; set; }

        public override string ToString()
        {
            return $"[{Id}] | {Year} - {Flagged} - {Content}";
        }
    }
}
