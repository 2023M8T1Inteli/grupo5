using System;
using System.Collections.Generic;
using System.Xml;

namespace LLEx
{
    // Represents a node in a syntax tree.
    public class SyntaxNode
    {
        // The name of the syntax node.
        public string Name { get; }

        // Dictionary to store attributes associated with the syntax node.
        private Dictionary<string, object> attributes = new Dictionary<string, object>();

        // Constructor to initialize the SyntaxNode with a given name.
        public SyntaxNode(string name)
        {
            Name = name;
        }

        // Method to add attributes to the syntax node.
        public void AddAttributes(string attributeName, object attributeValue)
        {
            // Add the attribute to the dictionary.
            attributes[attributeName] = attributeValue;
        }
        //Method: Converts the syntax node to an XML element.

        public XmlElement ToXmlElement(XmlDocument xmlDoc)
        {
            XmlElement element = xmlDoc.CreateElement(Name);

            foreach (var attribute in attributes)
            {
                if (attribute.Value is SyntaxNode childNode)
                {
                    element.AppendChild(childNode.ToXmlElement(xmlDoc));
                }
                else if (attribute.Value is SyntaxNodeLeaf leafNode)
                {
                    XmlElement leafElement = xmlDoc.CreateElement(leafNode.Name);
                    leafElement.InnerText = leafNode.Value;
                    element.AppendChild(leafElement);
                }
                else
                {
                    XmlElement attrElement = xmlDoc.CreateElement(attribute.Key);
                    attrElement.InnerText = attribute.Value.ToString();
                    element.AppendChild(attrElement);
                }
            }

            return element;
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
