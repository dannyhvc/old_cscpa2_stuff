using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterExample
{
    //This is used by DefaultInterface (the target)
    public class DefaultUser
    {
        private string Name;
        private string Email;

        public DefaultUser(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string GetName() { return Name; }
        public string GetEmail() { return Email; }
    }
}
