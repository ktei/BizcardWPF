using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LiteApp.Bizcard.Models
{
    public enum AddressType
    {
        [Description("Home")]
        Home = 0,
        [Description("Work")]
        Work,
        [Description("Other")]
        Other
    }
}
