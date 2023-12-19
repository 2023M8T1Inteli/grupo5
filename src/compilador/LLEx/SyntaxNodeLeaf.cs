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

        // The line associated with the syntax node leaf.
        public int Line { get; }

        // Constructor to initialize the SyntaxNodeLeaf with a given name and value.
        public SyntaxNodeLeaf(string name, string value, int line)
        {
            // Set the value and name properties based on the provided parameters.
            Value = value;
            Name = name;
            Line = line;
        }
    }
}
