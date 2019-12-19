using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Command
    {
        public static Server server;

        public void Start()
        {
            server.Start();
        }

        public void Stop()
        {
            server.Stop();
        }

        public void Send(string text)
        {
            server.SendMessage(text);
        }

        public void Ban(string name)
        {
            server.Ban(name);
        }

        public void UserList()
        {
            server.InfoClient();
        }

        public void Disconnect(Client client)
        {
            server.Disconnect(client);
        }
    }
}
