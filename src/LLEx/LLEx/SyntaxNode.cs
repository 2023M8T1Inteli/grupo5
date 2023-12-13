using System;
using System.Collections.Generic;
using System.Text;
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

        public object GetAttribute(string attributeName)
        {
            if (attributes.ContainsKey(attributeName))
            {
                return attributes[attributeName];
            }
            return null;
        }

        public IEnumerable<KeyValuePair<string, object>> GetAttributes()
        {
            return attributes;
        }

        public int CountAtributtes(){
            return attributes.Count;
        }

        public bool VerifyKey(string key){
            return attributes.ContainsKey(key);
        }

        // Method to add attributes to the syntax node.
        public void AddAttributes(string attributeName, object attributeValue)
        {
            // Add the attribute to the dictionary.
            attributes[attributeName] = attributeValue;
        }

        // Converts the syntax node to a string representation.
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            ToStringRecursive(this, stringBuilder, 0);
            return stringBuilder.ToString();
        }

        // Recursive helper method for building the string representation with indentation
        private void ToStringRecursive(SyntaxNode node, StringBuilder stringBuilder, int indentationLevel)
        {
            // Append indentation based on the depth of the node
            stringBuilder.Append(new string(' ', indentationLevel * 2));

            // Append the node name
            stringBuilder.AppendLine(node.Name);

            // Iterate through attributes and recursively build the string for child nodes
            foreach (var attribute in node.attributes)
            {
                if (attribute.Value is SyntaxNode childNode)
                {
        
                    ToStringRecursive(childNode, stringBuilder, indentationLevel + 1);
                }
                else if (attribute.Value is SyntaxNodeLeaf leafNode)
                {
                    stringBuilder.Append(new string(' ', (indentationLevel + 1) * 2));
                    stringBuilder.AppendLine($"Chave: {attribute.Key} |     {leafNode.Name}: {leafNode.Value} | Linha: {leafNode.Line}");
                }
                else
                {
                    stringBuilder.Append(new string(' ', (indentationLevel + 1) * 2));
                    stringBuilder.AppendLine($"{attribute.Key}: {attribute.Value}");
                }
            }
        }

        // Converts the syntax node to an XML element.
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
                }else if(attribute.Value == null){
                    continue;
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
    }
}
