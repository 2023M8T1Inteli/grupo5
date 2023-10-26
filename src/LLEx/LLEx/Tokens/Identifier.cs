using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLEx.Tokens
{
    internal class Identifier : Token
    {
        public override String Name { get => this.GetType().Name; }
        public override String Value { get; set; }

        public Identifier(String value)
        {
            this.Value = value;
        }
    }
}
