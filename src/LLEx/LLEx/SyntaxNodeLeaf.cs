using System;
using System.Collections.Generic;
using System.Xml;

namespace LLEx
{
    // Represents a leaf node in a syntax tree.
    public class SyntaxNodeLeaf
    {
        // The name of the syntax node leaf.
        public string Name { get; }

        // The value associated with the syntax node leaf.
        public string Value { get; }

        // Constructor to initialize the SyntaxNodeLeaf with a given name and value.
        public SyntaxNodeLeaf(string name, string value)
        {
            // Set the value and name properties based on the provided parameters.
            Value = value;
            Name = name;
        }
    }
}
