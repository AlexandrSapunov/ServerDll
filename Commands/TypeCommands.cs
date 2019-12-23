using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    public enum TypeCommand
    {
        Empty = -3,
        Unknow = -2,
        BadCommand = -1,
        Send,
        Ban,

        [Single]
        Help,

        [Single]
        UserList,

        [Single]
        Exit,
    }
}
