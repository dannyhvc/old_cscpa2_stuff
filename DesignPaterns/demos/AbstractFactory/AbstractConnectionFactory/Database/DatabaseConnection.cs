using System;
using System.Collections.Generic;
using System.Text;

using AbstractConnectionFactory.Interfaces;

namespace AbstractConnectionFactory.Database
{
    public class DatabaseConnection : IConnection
    {
        private readonly DatabaseHost _host;

        public DatabaseConnection(DatabaseHost host)
        {
            _host = host;
        }

        public string QueryHost()
        {
            return _host.EmployeeTable;
        }
    }
}
