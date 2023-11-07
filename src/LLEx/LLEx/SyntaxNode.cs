using System;
using System.Collections.Generic;
using System.Xml;

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

        public XmlElement ToXmlElement(XmlDocument xmlDoc)
        {
            XmlElement xmlElement = xmlDoc.CreateElement(string.IsNullOrEmpty(Name) ? "Node" : Name);
            if (!string.IsNullOrEmpty(Value))
            {
                xmlElement.InnerText = Value;
            }

            foreach (SyntaxNode child in Children)
            {
                xmlElement.AppendChild(child.ToXmlElement(xmlDoc));
            }

            return xmlElement;
        }

    }
}
