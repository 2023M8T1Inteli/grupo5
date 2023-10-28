using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLEx.Tokens
{
    internal class LPAR : Token
    {
        public override String Name { get => this.GetType().Name; }
        public override String Value { get; set; }

        public LPAR(String value, int line)
        {
            this.Value = value;
            this.Line = line;
        }
    }
}
