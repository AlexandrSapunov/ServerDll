using Server.Commands;
using System;
using System.Net;

namespace Server
{
    public class ServerWorker
    {
        public static Server Server;

        public ServerWorker()
        {
            CommandWorker.Start();
        }
        public void CmdWorker(string input)
        {
            var command = Commands.Commands.GetCommand(input);

            switch (command.Type)
            {
                case TypeCommand.Send:
                    CommandWorker.Send(command.FullValue);
                    break;
                case TypeCommand.Help:
                    CommandWorker.Help();
                    break;
                case TypeCommand.Ban:
                    CommandWorker.Ban(command.FullValue);
                    break;
                case TypeCommand.Exit:
                    CommandWorker.Exit();
                    break;
                case TypeCommand.UserList:
                    CommandWorker.UserList();
                    break;
                default:
                    break;
            }
        }
    }
}

