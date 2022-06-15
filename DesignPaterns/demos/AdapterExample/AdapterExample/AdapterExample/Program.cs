using System;

namespace AdapterExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();

            DefaultInterface loginInterface = new ObjectAdapter();
            loginInterface.Login(username, password);

            DefaultUser user = loginInterface.GetUser();
            if (user != null)
            {
                Console.WriteLine("Logged in - email is " + user.GetEmail());
            }
            else
            {
                Console.WriteLine("Login failed");
            }
        }
    }
}
