using System;
using System.Collections.Generic;
using System.Text;

using AbstractConnectionFactory.Interfaces;

namespace AbstractConnectionFactory.Socket
{
    public class SocketConnectionFactory : IConnectionFactory
    {
        public IConnection CreateConnection(IHost host)
        {
            return new SocketConnection(host as SocketHost);
        }

        public IHost CreateHost()
        {
            return new SocketHost("180.157.114.17");
        }
    }
}
