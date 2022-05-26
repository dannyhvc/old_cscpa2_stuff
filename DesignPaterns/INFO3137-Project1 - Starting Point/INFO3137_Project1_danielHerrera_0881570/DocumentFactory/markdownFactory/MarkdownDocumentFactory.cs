using DocumentFactory.markdownFactory.markdownDocument;
using DocumentFactory.markdownFactory.markdownElements;
using System;

namespace DocumentFactory.MarkdownUtil
{
    public class MarkdownDocumentFactory : IDocumentFactory
    {
        static MarkdownDocumentFactory _instance;

        private MarkdownDocumentFactory() { }

        public static MarkdownDocumentFactory Instance()
        {
            if (_instance == null)
                _instance = new MarkdownDocumentFactory();
            return _instance;
        }

        public IDocument CreateDocument(string fileName)
        {
            return new MarkdownDocument(fileName);
        }

        public IElement CreateElement(string elementType, string props)
        {
            // creates document using string element type then gives the props as attributes to the element
            switch (elementType)
            {
                case "Header":
                    return new MarkdownHeader(props);
                case "Image":
                    return new MarkdownImage(props);
                case "List":
                    return new MarkdownList(props);
                case "Table":
                    return new MarkdownTable(props);
                default:
                    throw new ArgumentException($"No Element of type : {elementType} exists");
            }
        }
    }
}
