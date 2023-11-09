using System;
using System.Collections.Generic;
using System.Xml;

namespace LLEx
{
    public class SyntaxNode
    {
        public string Name { get; }
        Dictionary<string, object> attributes = new Dictionary<string, object>();
        public SyntaxNode(string name)
        {
            Name = name;
        }

        public void AddAttributes(string name, object value)
        {
            attributes[name] =value;
        }

        public XmlElement ToXmlElement(XmlDocument xmlDoc)
        {
            XmlElement xmlElement = xmlDoc.CreateElement(string.IsNullOrEmpty(Name) ? "Node" : Name);
            if (!string.IsNullOrEmpty(Value))
            {
                xmlElement.InnerText = Value;
            }

            // foreach (SyntaxNode child in Children)
            // {
            //     xmlElement.AppendChild(child.ToXmlElement(xmlDoc));
            // }

            return xmlElement;
        }

    }
}
