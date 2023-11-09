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

    //     static XmlElement ToXmlElement(XmlDocument xmlDoc, XmlElement parentElement, Dictionary<string, object> attributes)
    // {
    //     XmlElement xmlElement = xmlDoc.CreateElement("Node");

    //     foreach (var attribute in attributes)
    //     {
    //         XmlElement attributeElement = xmlDoc.CreateElement(attribute.Key);
    //         attributeElement.InnerText = attribute.Value.ToString();
    //         xmlElement.AppendChild(attributeElement);
    //     }

    //     parentElement.AppendChild(xmlElement);

    //     return xmlElement;
    // }

    }
}
