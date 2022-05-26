using System;

using AbstractConnectionFactory.Interfaces;

namespace AbstractConnectionFactory
{
    class Program
    {
        // this method doesn't need to know anything about
        // the concrete type of factory, connection, or host
        // that it uses
        static void CreateAndQueryConnection(IConnectionFactory factory)
        {
            var host = factory.CreateHost();
            var connection = factory.CreateConnection(host);
            Console.WriteLine(connection.QueryHost());
        }

        static void Main(string[] args)
        {
            CreateAndQueryConnection(new Database.DatabaseConnectionFactory());
            CreateAndQueryConnection(new Socket.SocketConnectionFactory());
        }
    }
}
