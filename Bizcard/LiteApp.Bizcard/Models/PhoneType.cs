using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LiteApp.Bizcard.Models
{
    public enum PhoneType
    {
        [Description("Home")]
        Home = 0,
        [Description("Mobile")]
        Mobile,
        [Description("Work")]
        Work,
        [Description("Other")]
        Other
    }
}
