using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Server.Commands
{
    public static class CommandWorker
    {
        public static Server server;
        
        public static void Send(string message)
        {

            Console.WriteLine($"Sending message: {message}");
            server.SendMessage(message);
        }

        public static void Help()
        {
            Console.WriteLine("Help: Show list command");
            Console.WriteLine("Send: Send message");
            Console.WriteLine("Ban: Disconnect User");
            Console.WriteLine("Exit: Close client");
        }

        public static void Ban(string name)
        {
            server.Ban(name);
        }

        public static void UserList()
        {
            server.ClientInfo();
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }

        public static void Start(IPAddress iP)
        {
            server = new Server(iP,8080);
            server.Start();
        }
    }
}
