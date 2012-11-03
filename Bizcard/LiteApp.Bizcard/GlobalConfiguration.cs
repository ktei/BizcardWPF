using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using LiteApp.Bizcard.Framework;

namespace LiteApp.Bizcard
{
    [Export(typeof(IGlobalConfiguration))]
    public class GlobalConfiguration : IGlobalConfiguration
    {
        public DataSource DataSource
        {
            get { return DataSource.Mock; }
        }
    }
}
