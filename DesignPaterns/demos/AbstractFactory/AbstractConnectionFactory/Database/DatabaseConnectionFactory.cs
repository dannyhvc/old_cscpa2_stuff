using System;
using System.Collections.Generic;
using System.Text;

using AbstractConnectionFactory.Interfaces;

namespace AbstractConnectionFactory.Database
{
    public class DatabaseConnectionFactory : IConnectionFactory
    {
        public IConnection CreateConnection(IHost host)
        {
            return new DatabaseConnection(host as DatabaseHost);
        }

        public IHost CreateHost()
        {
            return new DatabaseHost("CompanyDatabase.EmployeeTable");
        }
    }
}
