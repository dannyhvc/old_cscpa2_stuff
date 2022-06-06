using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using project1_decllangs_daniel_herrera_0881570.backend;
using project1_decllangs_daniel_herrera_0881570.backend.schemaClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace project1_decllangs_daniel_herrera_0881570.frontend
{
    public class ChoiceInterface
    {
        public AccountManager Manager { get; set; }

        public ChoiceInterface(string data_file, string schema_file)
        {
            // print initial banner
            Console.WriteLine("PASSWORD MANAGEMENT SYSTEM\n");
            // file io
            // if file not exists create it and start program
            if (!File.Exists(data_file))
                using (FileStream fs = File.Create(data_file))
                    Manager = new AccountManager();
            else
                using (StreamReader dataFileReader = new StreamReader(data_file, Encoding.UTF8))
                {
                    // if file exists then populate the Accounts in the manager
                    string json_content = dataFileReader.ReadToEnd();
                    string schema_contents;

                    //validate schema and json data
                    using (StreamReader schemaFileReader = new StreamReader(schema_file, Encoding.UTF8))
                        schema_contents = schemaFileReader.ReadToEnd();

                    // parse schema and text
                    var schema = JSchema.Parse(schema_contents);
                    var json = JArray.Parse(json_content);

                    // validate and deserialize
                    if (json.IsValid(schema))
                    {
                        var deserializedProduct = JsonConvert.DeserializeObject<List<Account>>(json_content);
                        Manager = new AccountManager(deserializedProduct);
                    }
                    else
                    {
                        Console.WriteLine("Invalide json for the current schema. Reverting buffer to empty...\n\n");
                        Manager = new AccountManager();
                    }

                }
            MainMenuSequence();
        }

        private void ShowAllAccounts()
        {
            // listing as in terminal all the acounts in index order
            Console.WriteLine("===================== Account Entries =====================");

            foreach (var it in Manager.Accounts.Select(
                (Account Value, int Index) => new { Value, Index = Index + 1 })
            )
                Console.WriteLine($"{it.Index}. {it.Value.Description}");
            Console.WriteLine("===========================================================");
        }

        private void ShowMainMenu()
        {
            // listing main menu choices
            Console.WriteLine("===========================================================");
            Console.WriteLine("Press # from the above list to select an entry.");
            Console.WriteLine("Press A to add a new entry.");
            Console.WriteLine("Press X to quit.");
            Console.WriteLine("===========================================================\n");

        }

        private void GetMainMenu()
        {
            // stream input for main menu
            // recursive

            Console.Write("Enter a command: ");
            var choice = Console.ReadLine().Trim();

            if (choice.All(char.IsDigit) && choice != "\n")
            {
                int index_choice = Convert.ToInt32(choice) - 1;
                if (index_choice >= Manager.Accounts.Count)
                {
                    Console.Write("Number out of list range! Try Again...\n");
                    GetMainMenu();
                }
                else
                {
                    Manager.Select(index_choice);
                    AccountMenuSequence(index_choice);
                }
            }
            else if (choice.ToUpper() == "A")
            {
                GetAddEntryForm();
                MainMenuSequence();
            }
            else if (choice.ToUpper() == "X")
            {
                ClosingSequence();
            }
            else
            {
                Console.Write("Invalid command! Try Again...\n");
                GetMainMenu();
            }
        }

        private void MainMenuSequence()
        {
            ShowAllAccounts();
            ShowMainMenu();
            GetMainMenu();
        }

        private void ShowCurrentAccount(int index)
        {
            // listing the selected account
            Console.WriteLine("***********************************************************");
            Console.WriteLine($"{index + 1}. {Manager.Selected.Description}");
            Console.WriteLine("***********************************************************");
            Console.WriteLine($"User ID: {Manager.Selected.UserId}");
            Console.WriteLine($"Password: {Manager.Selected.Password.Value}");
            Console.WriteLine($"Password Strength: {Manager.Selected.Password.StrengthText} ({Manager.Selected.Password.StrengthNum}%)");
            Console.WriteLine($"Passwrod Reset: {Manager.Selected.Password.LastReset}");
            Console.WriteLine($"Login Uri: {Manager.Selected.LoginUrl}");
            Console.WriteLine("***********************************************************");
        }

        private void ShowAccountMenu()
        {
            // listing the account menu
            Console.WriteLine("***********************************************************");
            Console.WriteLine("Press P to change this password");
            Console.WriteLine("Press D to delete this entry.");
            Console.WriteLine("Press M to return to main menu");
            Console.WriteLine("***********************************************************\n");
        }

        private void GetAccountMenu()
        {
            // stream input for AccountMenu 
            // recursive
            Console.Write("Enter a command: ");
            var choice = Console.ReadLine();

            switch (choice.ToUpper())
            {
                case "P":
                    GetChangePasswordForm();
                    MainMenuSequence();
                    break;
                case "D":
                    GetDeleteAccountEntry();
                    MainMenuSequence();
                    break;
                case "M":
                    MainMenuSequence();
                    break;
                default:
                    Console.WriteLine("Invalid command! Try Again...\n");
                    break;
            }
            GetAccountMenu();
        }

        private void AccountMenuSequence(int index)
        {
            ShowCurrentAccount(index);
            ShowAccountMenu();
            GetAccountMenu();
        }

        private void GetDeleteAccountEntry()
        {
            // stream input for creating a new account entry
            Console.Write("Delete? (Y/N): ");
            var yn = Console.ReadLine();
            switch (yn.ToUpper())
            {
                case "Y":
                    Manager.Delete();
                    return;
                case "N":
                    return;
                default:
                    Console.WriteLine("Invalid command! Try Again...\n");
                    break;
            }
            GetDeleteAccountEntry();
        }

        private void GetAddEntryForm()
        {
            // stream input for creating a new account entry
            Console.WriteLine("Please key-in the following fields...\n");

            Console.Write("Description: ");
            var description = Console.ReadLine();
            Console.Write("User id: ");
            var userid = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();
            Console.Write("Login Url: ");
            var login_url = Console.ReadLine();
            Console.Write("Account #: ");
            var account_num = Console.ReadLine();
            Console.Write("\n");

            if (!Manager.Insert(description, userid, password, login_url, account_num))
            {
                Console.WriteLine("ERROR: Invalid account information entered. Please try again.\n");
                GetAddEntryForm();
            }
        }

        private void GetChangePasswordForm()
        {
            // stream input for changing password
            Console.Write("New Password: ");
            var new_password = Console.ReadLine();
            Manager.Update(new_password);
        }

        private void ClosingSequence()
        {
            string output = JsonConvert.SerializeObject(Manager.Accounts);
            using (StreamWriter file = new StreamWriter("data.json"))
                file.WriteLine(output);

            Console.Write("Saved changes! Goodbye!\n");
            Environment.Exit(0);
        }

    }
}
