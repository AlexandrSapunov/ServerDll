using System;

namespace Server
{
    public class Server
    {
        public static ServerWorker server;
        public static void Send(string message)
        {
            Console.WriteLine($"Sending message: {message}");
            server.SendMessage(message);
        }

        public static void Ban(string name)
        {
            server.Ban(name);
        }

        public static void UserList()
        {
            server.ClientInfo();
        }

        public static void Start()
        {
            server = new ServerWorker();
            server.Start();
        }

        public static void Stop()
        {
            server.Stop();
        }
    }
}
