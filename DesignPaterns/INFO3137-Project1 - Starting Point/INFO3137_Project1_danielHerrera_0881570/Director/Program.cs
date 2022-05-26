using DocumentFactory;
using DocumentFactory.HTMLUtil;
using DocumentFactory.MarkdownUtil;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Director
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] commands;
            var list = File.ReadAllText("CreateDocumentScript.txt");
            commands = list.Split('#');

            // definition of factories and document before the loop so they don't get updated per command but per switch case
            IDocumentFactory documentFactory = null;
            IDocument document = null;

            foreach (var command in commands)
            {
                var strippedCommand = Regex.Replace(command, @"\t|\n|\r", "");
                var commandList = strippedCommand.Split(':');

                switch (commandList[0])
                {
                    case "Document":
                        // Your document creation code goes here
                        const int FILETYPE_IDX = 0;
                        const int FILENAME_IDX = 1;
                        const int DOC_ARGS_IDX = 1;
                        string[] doc_args = commandList[DOC_ARGS_IDX].Split(';');

                        // Getting instance of doc based on parsed file type
                        if (doc_args[FILETYPE_IDX] == "Html")
                            documentFactory = HTMLDocumentFactory.Instance();
                        else if (doc_args[FILETYPE_IDX] == "Markdown")
                            documentFactory = MarkdownDocumentFactory.Instance();
                        else
                            throw new ArgumentException($"No file type of {doc_args[FILETYPE_IDX]}");

                        // creating a new document with given instance
                        document = documentFactory.CreateDocument(doc_args[FILENAME_IDX]);
                        break;

                    case "Element":
                        // Your element creation code goes here
                        const int ELEMENT_TYPE_IDX = 1;
                        const int ELEMENT_PROPS_IDX = 2;
                        // creating elements per parsed string
                        IElement element = documentFactory.CreateElement(
                            elementType: commandList[ELEMENT_TYPE_IDX],
                            props: commandList[ELEMENT_PROPS_IDX]
                        );
                        document.AddElement(element);
                        break;
                    case "Run":
                        // Your document running code goes here
                        document.RunDocument();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}