using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    public class Command
    {
        public TypeCommand Type { get; set; }
        public int ParamCount { get; set; }
        public string[] Values { get; set; } = new string[0];
        public string FullValue { get; set; }
    }
}
