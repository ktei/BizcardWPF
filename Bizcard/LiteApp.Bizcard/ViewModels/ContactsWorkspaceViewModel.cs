using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using LiteApp.Bizcard.Framework;
using System.ComponentModel.Composition;
using LiteApp.Bizcard.Data;
using LiteApp.Bizcard.Helpers;
using System.Collections.ObjectModel;
using System.Threading;

namespace LiteApp.Bizcard.ViewModels
{
    [Export(typeof(IWorkspace))]
    [ExportMetadata("Name", "Contacts")]
    public class ContactsWorkspaceViewModel : Conductor<PropertyChangedBase>.Collection.OneActive, IWorkspace
    {
        IContactRepository _contactRepository;
        bool _isBusy;
        bool _nothingIsSelected;

        [Import]
        public RepositoryFactory RepositoryFactory { get; set; }

        [Import]
        public IGlobalConfiguration Configuration { get; set; }

        public IContactRepository ContactRepository
        {
            get
            {
                if (_contactRepository == null)
                {
                    _contactRepository = RepositoryFactory.GetRepository<IContactRepository>();
                }
                return _contactRepository;
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            private set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    NotifyOfPropertyChange(() => IsBusy);
                }
            }
        }

        public bool NothingIsSelected
        {
            get { return _nothingIsSelected; }
            private set
            {
                if (_nothingIsSelected != value)
                {
                    _nothingIsSelected = value;
                    NotifyOfPropertyChange(() => NothingIsSelected);
                }
            }
        }

        public override void ActivateItem(PropertyChangedBase item)
        {
            NothingIsSelected = item == null;
            base.ActivateItem(item);
        }

        protected override void OnInitialize()
        {
            LoadContacts();
        }

        void LoadContacts()
        {
            if (IsBusy)
                return;
            System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    try
                    {
                        IsBusy = true;
                        if (Configuration.MockDelay)
                        {
                            Thread.Sleep(Configuration.DelayInterval);
                        }
                        var data = ContactRepository.GetContacts();
                        Items.AddRange(data.Select(x => new ContactViewModel(x)));
                        if (Items.Count > 0)
                        {
                            ActivateItem(Items[0]);
                        }
                    }
                    catch
                    {
                        // TODO: handle exception properly
                        throw;
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                });
        }
    }
}
