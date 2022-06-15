using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterExample
{
    public class GoogleUsersAPI
    {
        //This is the "adaptee" - the class that we need an adapter to adapt to the target (DefaultInterface)
        public GoogleUser LoginWithEmailAndPassword(string username, string password)
        {
            // run some http querying maybe...
            // could be complex and we want to isolate this from the client
            if (username == "bsmith" && password == "password")
            {
                return new GoogleUser("Bob", "bob@gmail.ca");
            }

            throw new Exception("I'm Google: bad username/password");
        }
    }
}
