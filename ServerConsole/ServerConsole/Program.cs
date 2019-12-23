using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using System.Net;

namespace ServerConsole
{
    class Program
    {
        public static ServerWorker worker;
        static void Main(string[] args)
        {
            worker = new ServerWorker();

            bool Repeat = true;
            while (Repeat)
            {
                string input = Console.ReadLine();
                worker.CmdWorker(input);
            }
        }
    }
}
