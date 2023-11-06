using System;
using System.Collections.Generic;

namespace LLEx
{
    public class SyntaxNode
    {
        public string Name { get; }
        public string Value { get; set; }
        public List<SyntaxNode> Children { get; } = new List<SyntaxNode>();

        public SyntaxNode(string name)
        {
            Name = name;
        }

        public SyntaxNode(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public void AddChild(SyntaxNode child)
        {
            Children.Add(child);
        }

        private void PrintSyntaxNode(SyntaxNode node, string indent)
        {
            Console.WriteLine(indent + node.Name + (string.IsNullOrEmpty(node.Value) ? "" : $" ({node.Value})"));
            foreach (var child in node.Children)
            {
                PrintSyntaxNode(child, indent + "  ");
            }
        }

    }
}
