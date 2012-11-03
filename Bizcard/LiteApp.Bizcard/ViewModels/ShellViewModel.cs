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
        #region Properties

        [ImportMany]
        public IEnumerable<Lazy<IWorkspace, IWorkspaceMetadata>> Workspaces { get; set; }

        public override string DisplayName
        {
            get
            {
                return ApplicationStrings.ApplicationName;
            }
            set
            {
                base.DisplayName = value;
            }
        }

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
