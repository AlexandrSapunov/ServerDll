using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ServerWorker
    {
        public static Server Server;
        public ServerWorker(string input)
        {
            while (true)
            {
                try
                {
                    if (input == "exit")
                    {
                        Environment.Exit(0);
                    }
                    else if (input == "ban")
                    {
                        string name = Console.ReadLine();
                        Server.Ban(name);
                        Server.SendMessage(name + " ban");
                    }
                    else if (input == "stop")
                    {
                        Server.Stop();
                    }
                    else
                    {
                        Server.SendMessage(input);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
