using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractConnectionFactory.Interfaces
{
    public interface IConnectionFactory
    {
        IHost CreateHost();
        IConnection CreateConnection(IHost host);
    }
}
