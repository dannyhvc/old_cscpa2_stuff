using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterExample
{
    /// <summary>
    //This is a class adapter, which is (less common) way you can structure an adapter 
    /// </summary>

    public class ClassAdapter : GoogleUsersAPI, DefaultInterface
    {
        private DefaultUser defaultUser;
        public DefaultUser GetUser()
        {
            return defaultUser;
        }

        public void Login(string username, string password)
        {
            try
            {
                // need to call API method to log in
                GoogleUser user = LoginWithEmailAndPassword(username, password);
                // need to change the Google user into an Auth user
                defaultUser = new DefaultUser(user.GetName(), user.GetEmail());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
