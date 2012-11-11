using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace LiteApp.Bizcard.Framework
{
    public interface IShell : IConductActiveItem
    {
        void ActivateWorkspace(string name);
    }
}
