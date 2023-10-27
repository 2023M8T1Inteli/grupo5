using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLEx
{
    abstract class Token
    {
        public abstract string Name { get; }
        public abstract string Value { get; set; }
        public int Line { get; set; }
        public override string ToString()
        {
            return $"<{Name} line={Line}>{Value}</{Name}>\n";
        }
    }
}
