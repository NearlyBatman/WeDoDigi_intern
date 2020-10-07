using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeDoDigi_intern.Models
{
    public class TagHolder
    {
        public List<string> tagHolder = new List<string>();
        public List<string> tagMarkedHolder = new List<string>();
        public TagHolder()
        {

        }
        public TagHolder(List<string> tMark, List<string> tHold)
        {
            this.tagHolder = tHold;
            this.tagMarkedHolder = tMark;
        }
    }
}
