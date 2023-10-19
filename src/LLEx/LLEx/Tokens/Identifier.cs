using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLEx.Tokens
{
    internal class Identifier : Token
    {
        public override string Name { get => this.GetType().Name; }
        public override string Value { get; }

        public Identifier(string value)
        {
            this.Value = value;
        }
    }
}
