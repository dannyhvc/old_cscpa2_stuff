using System;
using System.Collections.Generic;
using System.Text;

using AbstractConnectionFactory.Interfaces;

namespace AbstractConnectionFactory.Socket
{
    public class SocketHost : IHost
    {
        private readonly string _ipAddress;

        public SocketHost(string location)
        {
            _ipAddress = location;
        }

        public string GetConnectionStatus()
        {
            return $"Socket connection to {_ipAddress} is open\n";
        }
    }
}
