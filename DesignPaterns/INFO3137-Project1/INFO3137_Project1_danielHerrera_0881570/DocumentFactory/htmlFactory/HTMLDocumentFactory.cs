using DocumentFactory.htmlFactory.htmlDocument;
using DocumentFactory.htmlFactory.htmlElements;
using System;

namespace DocumentFactory.HTMLUtil
{
    public class HTMLDocumentFactory : IDocumentFactory
    {
        private static HTMLDocumentFactory _instance = null;

        private HTMLDocumentFactory() { }

        public static HTMLDocumentFactory Instance()
        {
            // lazy init
            if (_instance == null)
                _instance = new HTMLDocumentFactory();
            return _instance;
        }

        // interface implemented methods
        public IDocument CreateDocument(string fileName)
        {
            return new HTMLDocument(fileName);
        }

        public IElement CreateElement(string elementType, string props)
        {
            // creates document using string element type then gives the props as attributes to the element
            switch (elementType)
            {
                case "Header":
                    return new HTMLHeader(props);
                case "Image":
                    return new HTMLImage(props);
                case "List":
                    return new HTMLList(props);
                case "Table":
                    return new HTMLTable(props);
                default:
                    throw new ArgumentException($"No Element of type : {elementType} exists");
            }
        }
    }
}
