using System;
using System.Collections.Generic;
using System.Xml;

namespace LLEx
{
    public class SyntaxTree
    {
        public SyntaxNode Root { get; }

        public SyntaxTree(SyntaxNode root)
        {
            Root = root;
        }

        // public XmlDocument ToXmlDocument()
        // {
        //     XmlDocument xmlDoc = new XmlDocument();
        //     XmlElement rootElement = Root.ToXmlElement(xmlDoc);
        //     xmlDoc.AppendChild(rootElement);
        //     return xmlDoc;
        // }

    }

}
