using DocumentBuilderLibrary.director;

namespace ConsoleClient
{
    public class CommandClient
    {
        Director? _director;
        const string _INVALID_INPUT = "\nInvalid input. For Usage, type 'Help'\n";

        public CommandClient()
        {
            PrintHelp(); // start by letting the user know the options
        }

        void PrintHelp()
        {
            string[] commands =
            {
                "help",
                "mode:<JSON|XML>",
                "branch:<name>",
                "leaf:<name>:<content>",
                "close",
                "print",
                "exit"
            };
            string[] descriptions =
            {
                "-Prints Usage (this page)",
                "-Sets mode to JSON or XML. Must be set before creating or closing.",
                "-Creates a new branch, assigning it the passed name.",
                "-Creates a new leaf, assigning the passed name and content.",
                "-Closes the current branch, as long as it is not the root.",
                "-Prints the doc in its current state to the console.",
                "-Exits the application."
            };

            Console.WriteLine("Usage:");
            // using WriteLines built in padding formatting so that the print will end up evenly spaced
            for (int i = 0; i < commands.Length; i++)
                Console.WriteLine("\t{0,-22} {1}", commands[i], descriptions[i]);
            Console.WriteLine();
        }

        string GetInput()
        {
            Console.Write("> ");
            return Console.ReadLine()!;
        }

        bool CheckMode()
        {
            // utility method used to make sure that there is a mode set
            bool retval = _director != null;
            if (!retval)
                Console.WriteLine("Error. Mode has not been set. For usage, type 'Help'");
            return retval;
        }

        public void Run()
        {
            // lambda function for checking that the amount or args matches
            // the expected amount for a given command.
            Func<(string[] arg, int expected), bool> testArgLen = x =>
            {
                var retval = x.arg.Length == x.expected;
                if (!retval)
                    Console.WriteLine(_INVALID_INPUT);
                return retval;
            };

            // event loop
            for (; ; )
            {
                var input = GetInput(); // input request

                // making sure we actually get an input
                if (string.IsNullOrEmpty(input))
                    Console.WriteLine(_INVALID_INPUT);

                // checking if its a parsed input
                else if (input.Contains(':'))
                {
                    string[] command_items = input.Split(':');

                    switch (command_items[0].ToUpper())
                    {
                        case "MODE":
                            if (testArgLen((command_items, 2)))
                                _director = new Director(command_items[1]);
                            break;
                        case "BRANCH":
                            // making sure the correct amount of args are passed as well as 
                            if (testArgLen((command_items, 2)) && CheckMode())
                            {
                                _director!.Name = command_items[1];
                                _director.BuildBranch();
                            }
                            break;
                        case "LEAF":
                            // making sure the correct amount of args are passed as well as mode
                            // being set so that we can build it
                            if (testArgLen((command_items, 3)) && CheckMode())
                            {
                                _director!.Name = command_items[1];
                                _director.Content = command_items[2];
                                _director.BuildLeaf();
                            }
                            break;
                        default:
                            Console.WriteLine(_INVALID_INPUT);
                            break;
                    }//switch
                }
                else // branching for unparsed inputs
                    switch (input.ToUpper())
                    {
                        case "HELP":
                            PrintHelp();
                            break;
                        case "CLOSE":
                            if (CheckMode())
                                _director!.CloseBranch();
                            break;
                        case "PRINT":
                            if (CheckMode())
                                Console.WriteLine(_director!.Builder.GetDocument().Print(0));
                            break;
                        case "EXIT":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine(_INVALID_INPUT);
                            break;
                    }// switch
            }//for
        }//Run
    }
}
