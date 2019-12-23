using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    public static class Commands
    {
        public static Command GetCommand(string userInput)
        {
            var splitResult = userInput.Split(' ');
            if (splitResult.Length == 0)
            {
                return new Command { Type = TypeCommand.Empty };
            }

            var values = (TypeCommand[])Enum.GetValues(typeof(TypeCommand));
            foreach (var item in values)
            {
                if ((int)item < 0)
                    continue;

                string enumName = item.ToString();

                if (string.Compare(enumName, splitResult[0], true) == 0)
                {
                    int userValuesLength = splitResult.Length - 1;
                    if (userValuesLength <= 0 && !SingleAttribute.GetSingleFlag(item))
                    {
                        Console.WriteLine("BAD COMMAND need more values after command");
                        return new Command { Type = TypeCommand.BadCommand };
                    }

                    string value = "";
                    string[] valueArray = new string[userValuesLength];
                    for (int i = 1; i < splitResult.Length; i++)
                    {
                        value += splitResult[i];
                        valueArray[i - 1] = splitResult[i];
                        if (i != splitResult.Length - 1)
                            value += " ";
                    }

                    return new Command
                    {
                        Type = item,
                        FullValue = value,
                        Values = valueArray,
                    };
                }
            }

            Console.WriteLine("Unknown command, please use real command");
            return new Command { Type = TypeCommand.Unknow };
        }
    }
}
