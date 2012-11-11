using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.Sterling;
using System.ComponentModel;
using System.Diagnostics;
using Wintellect.Sterling.Server.FileSystem;
using LiteApp.Bizcard.Models;

namespace LiteApp.Bizcard.Data.Sterling
{
    public sealed class SterlingService : IDisposable
    {
        private SterlingEngine _engine;
        private static readonly ISterlingDriver _driver = new FileSystemDriver("Bizcard/");

        public static SterlingService Current { get; private set; }

        public ISterlingDatabaseInstance Database { get; private set; }

        private SterlingDefaultLogger _logger;

        public void StartService()
        {
            _engine = new SterlingEngine();

            if (Debugger.IsAttached)
            {
                _logger = new SterlingDefaultLogger(SterlingLogLevel.Verbose);
            }

            _engine.Activate();
            Database = _engine.SterlingDatabase.RegisterDatabase<BizcardDatabase>(_driver);
            Database.RegisterTrigger(new IdentityTrigger<Contact>(Database));

            Current = this;
        }

        public void Dispose()
        {
            if (_engine != null)
            {
                _engine.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
