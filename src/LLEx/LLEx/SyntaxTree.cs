using System;
using System.Collections.Generic;

namespace LLEx
{
    public class SyntaxTree
    {
        public SyntaxNode Root { get; }

        public SyntaxTree(SyntaxNode root)
        {
            Root = root;
        }

        public void Print()
        {
            PrintTree(Root, "");
        }

        private void PrintTree(SyntaxNode node, string indent)
        {
            Console.WriteLine(indent + node.Name);
            foreach (var child in node.Children)
            {
                PrintTree(child, indent + "  ");
            }
        }

        public void PrintSyntaxTree(SyntaxTree syntaxTree)
        {
            PrintSyntaxNode(syntaxTree.Root, "");
        }
    }

}
