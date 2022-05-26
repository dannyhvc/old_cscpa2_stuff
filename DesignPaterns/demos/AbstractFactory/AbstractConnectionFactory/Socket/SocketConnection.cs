using System;
using System.Collections.Generic;
using System.Text;

using AbstractConnectionFactory.Interfaces;

namespace AbstractConnectionFactory.Socket
{
    public class SocketConnection : IConnection
    {
        private readonly SocketHost _host;

        public SocketConnection(SocketHost host)
        {
            _host = host;
        }

        public string QueryHost()
        {
            return _host.GetConnectionStatus();
        }
    }
}
