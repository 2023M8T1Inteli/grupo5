using System;
using System.Collections.Generic;
using System.Xml;

namespace LLEx
{
    public class SyntaxNodeLeaf
    {
        public string Name { get; }
        public string Value { get; }
        public SyntaxNodeLeaf(string name, string value)
        {   
            Value = value;
            Name = name;
        }


        

    }
}
