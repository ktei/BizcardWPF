using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteApp.Bizcard.Framework;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using LiteApp.Bizcard.Resources;
using LiteApp.Bizcard.Data;

namespace LiteApp.Bizcard.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell
    {
        public ShellViewModel()
        {
            DisplayName = ApplicationStrings.ApplicationName;
        }

        #region Properties

        [ImportMany]
        public IEnumerable<Lazy<IWorkspace, IWorkspaceMetadata>> Workspaces { get; set; }

        [Import]
        public Lazy<SettingsViewModel> Settings { get; set; }

        [Import]
        public Lazy<IWindowManager> WindowManager { get; set; }

        public Action<ThemeViewModel> ThemeSelected { get; set; }

        #endregion // Properties

        #region Public Methods

        public void ActivateWorkspace(string name)
        {
            var workspace = Workspaces.FirstOrDefault(x => x.Metadata.Name == name);
            if (workspace != null)
            {
                ActivateItem(workspace.Value);
            }
        }

        public void ChangeSettings()
        {
            WindowManager.Value.ShowDialog(Settings.Value);
        }

        #endregion // Public Methods

        #region Protected Methods

        protected override void OnInitialize()
        {
            ActivateWorkspace("Contacts");
            base.OnInitialize();
        }

        #endregion // Protected Methods
    }
}
