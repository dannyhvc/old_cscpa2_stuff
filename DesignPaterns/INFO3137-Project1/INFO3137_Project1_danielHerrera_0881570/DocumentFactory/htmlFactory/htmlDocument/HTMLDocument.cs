using System;
using System.Collections.Generic;
using System.IO;

namespace DocumentFactory.htmlFactory.htmlDocument
{
    public class HTMLDocument : IDocument
    {
        readonly string _filename;
        readonly List<IElement> _elements;
        readonly string _filepath;

        public HTMLDocument(string filename)
        {
            _filename = filename;
            _elements = new List<IElement>();
            _filepath = $"{Directory.GetCurrentDirectory()}\\{_filename}";
        }

        public void AddElement(IElement element)
        {
            // adds element to the document list
            _elements.Add(element);
        }

        public void RunDocument()
        {
            // When implemented in concrete HTML and Markdown Document classes, this
            // should generate either an HTML document and open it in chrome.
            if (!File.Exists($"{Directory.GetCurrentDirectory()}\\{_filename}"))
            {
                using (FileStream fs = File.Create(_filepath)) { }

                using (StreamWriter sw = File.AppendText(_filepath))
                    foreach (IElement element in _elements)
                        sw.WriteLine(element.ToString());
            }
            else
            {
                // if file does exist then empty, then overwrite the file with the new elements
                File.WriteAllText(_filepath, string.Empty);
                using (StreamWriter sw = File.AppendText(_filepath))
                    foreach (IElement element in _elements)
                        sw.WriteLine(element.ToString());
            }

            // start chrome with document file loaded
            if (_filename != null)
                System.Diagnostics.Process.Start($"{Directory.GetCurrentDirectory()}\\{_filename}", "chrome.exe");
            else
                throw new Exception("No Filename Provided");
        }
    }
}
