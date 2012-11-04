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
        static Random _random = new Random();

        public DataSource DataSource
        {
            get { return DataSource.Mock; }
        }

        public bool MockDelay
        {
            get { return true; }
        }

        public int DelayInterval
        {
            get { return _random.Next(500, 3000); }
        }
    }
}
