using System;
using System.Collections.Generic;
using System.Text;

using AbstractConnectionFactory.Interfaces;

namespace AbstractConnectionFactory.Database
{
    public class DatabaseHost : IHost
    {
        private readonly string _connectionString;

        public string EmployeeTable;

        public DatabaseHost(string location)
        {
            _connectionString = location;
            EmployeeTable = $"SELECT * FROM {_connectionString};\n\n" +
                    "**************************\n" +
                    "* ID * Name * Department *\n" +
                    "**************************\n" +
                    "* 1  * Joe  * Marketing  *\n" +
                    "* 2  * Mary * Accounting *\n" +
                    "**************************\n";

        }
    }
}
