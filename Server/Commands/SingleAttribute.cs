using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SingleAttribute : Attribute
    {
        public static bool GetSingleFlag(TypeCommand enums)
        {
            MemberInfo memberInfo = typeof(TypeCommand).GetMember(enums.ToString()).FirstOrDefault();

            if (memberInfo != null)
            {
                SingleAttribute attribute = (SingleAttribute)
                             memberInfo.GetCustomAttributes(typeof(SingleAttribute), false).FirstOrDefault();

                if (attribute != null)
                    return true;
            }

            return false;
        }
    }
}
