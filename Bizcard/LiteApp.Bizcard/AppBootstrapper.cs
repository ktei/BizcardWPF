using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using LiteApp.Bizcard.Framework;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using System.ComponentModel.Composition;
using LiteApp.Bizcard.Data.Sterling;

namespace LiteApp.Bizcard
{
    public class AppBootstrapper : Bootstrapper<IShell>
    {
        CompositionContainer _container;
        SterlingService _sterlingService;

        #region Properties

        public CompositionContainer Container
        {
            get { return _container; }
        }

        #endregion // Properties

        #region Protected Methods

        protected override void Configure()
        {
            ConfigureContainer();
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            base.OnStartup(sender, e);
            _sterlingService = new SterlingService();
            _sterlingService.StartService();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            SterlingService.Current.Dispose();
            base.OnExit(sender, e);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = _container.GetExportedValues<object>(contract);

            if (exports.Count() > 0)
                return exports.First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance)
        {
            _container.SatisfyImportsOnce(instance);
        }

        #endregion // Protected Methods

        #region Private Methods

        void ConfigureContainer()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            _container = new CompositionContainer(catalog);

            var batch = new CompositionBatch();
            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            _container.Compose(batch);
        }

        #endregion // Private Methods
    }
}
