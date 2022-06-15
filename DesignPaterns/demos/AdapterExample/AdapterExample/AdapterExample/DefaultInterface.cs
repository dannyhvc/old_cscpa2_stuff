using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterExample
{
    //This is the "target" - the interface that the client wants to use    
    public interface DefaultInterface
    {
            void Login(string username, string password);
            DefaultUser GetUser();
    }
}
