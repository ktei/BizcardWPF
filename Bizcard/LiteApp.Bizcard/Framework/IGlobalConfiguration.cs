using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteApp.Bizcard.Framework
{
    public interface IGlobalConfiguration
    {
        DataSource DataSource { get; }
        bool MockDelay { get; }
        int DelayInterval { get; }
    }
}
