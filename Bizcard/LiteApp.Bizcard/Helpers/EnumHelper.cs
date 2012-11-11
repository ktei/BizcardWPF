using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Resources;
using LiteApp.Bizcard.Resources;

namespace LiteApp.Bizcard.Helpers
{
    public static class EnumHelper
    {
        static ResourceManager rm = EnumStrings.ResourceManager;

        /// <summary>
        /// Retrieve the description on the enum, e.g.
        /// [Description("Bright Pink")]
        /// BrightPink = 2,
        /// Then when you pass in the enum, it will retrieve the description
        /// </summary>
        /// <param name="en">The Enumeration</param>
        /// <returns>A string representing the friendly name</returns>
        public static string GetDescription(this Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    var token = ((DescriptionAttribute)attrs[0]).Description;
                    return rm.GetString(token);
                }
            }

            return en.ToString();
        }

    }
}
