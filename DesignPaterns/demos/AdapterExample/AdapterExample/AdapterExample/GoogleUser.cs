using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterExample
{
    //This is used by GoogleUsersAPI (the adaptee)
    public class GoogleUser
    {
        private string Name;
        private string Email;

        public GoogleUser(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string GetName() { return Name; }
        public string GetEmail() { return Email; }
    }
}
