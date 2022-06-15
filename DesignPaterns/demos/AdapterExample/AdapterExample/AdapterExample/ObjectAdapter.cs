using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterExample
{
    /// <summary>
    ///This is an object adapter, which is a more common way of structuring an adapter
    /// </summary>
    public class ObjectAdapter : DefaultInterface
    {
        private GoogleUsersAPI googleAPI;
        private DefaultUser defaultUser;

        public ObjectAdapter()
        {
            googleAPI = new GoogleUsersAPI();
        }
        public DefaultUser GetUser()
        {
            return defaultUser;
        }

        public void Login(string username, string password)
        {
            try
            {
                // need to call API method to log in
                GoogleUser user = googleAPI.LoginWithEmailAndPassword(username, password);
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
