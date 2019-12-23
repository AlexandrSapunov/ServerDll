using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
 
namespace Server.Commands
{
    public class CmdInput
    {
        public CmdInput(string input)
        {
            var command = Commands.GetCommand(input);

            switch (command.Type)
            {
                case TypeCommand.Send:
                    CommandWorker.Send(command.FullValue);
                    break;
                case TypeCommand.Ban:
                    CommandWorker.Ban(command.FullValue);
                    break;
                case TypeCommand.Help:
                    CommandWorker.Help();
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
