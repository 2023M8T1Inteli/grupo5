using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLEx.Tokens
{
    internal class FACA : Token
    {
        public override String Name { get => this.GetType().Name; }
        public override String Value { get; set; }

        public FACA(String value, int line)
        {
            this.Line = line;
            this.Value = value;
        }
    }
}
