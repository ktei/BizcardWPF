using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using LiteApp.Bizcard.Resources;

namespace LiteApp.Bizcard.Models
{
    public enum AddressType
    {
        [Description("AddressType_Home")]
        Home = 0,
        [Description("AddressType_Work")]
        Work,
        [Description("AddressType_Other")]
        Other
    }
}
