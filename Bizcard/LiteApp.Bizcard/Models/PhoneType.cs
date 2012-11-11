using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LiteApp.Bizcard.Models
{
    public enum PhoneType
    {
        [Description("PhoneType_Home")]
        Home = 0,
        [Description("PhoneType_Mobile")]
        Mobile,
        [Description("PhoneType_Work")]
        Work,
        [Description("PhoneType_Other")]
        Other
    }
}
