using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tweets
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ContainsCharacterAttribute : Attribute
    {
        public string CharactersToBeChecked { get; set; } // could be char[] as well...
        public ContainsCharacterAttribute(string charactersToBeChecked)
        {
            this.CharactersToBeChecked = charactersToBeChecked;
        }
    }
}
