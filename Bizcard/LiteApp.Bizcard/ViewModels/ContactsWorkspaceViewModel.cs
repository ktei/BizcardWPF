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

namespace LiteApp.Bizcard.ViewModels
{
    [Export(typeof(IWorkspace))]
    [ExportMetadata("Name", "Contacts")]
    public class ContactsWorkspaceViewModel : Conductor<Screen>, IWorkspace
    {
        IContactRepository _contactRepository;
        BindableCollection<ContactViewModel> _contactsSource;
        ReadOnlyObservableCollection<ContactViewModel> _contactsReadOnly;
        bool _isBusy;

        
        [Import]
        public RepositoryFactory RepositoryFactory { get; set; }

        [Import]
        public IGlobalConfiguration Configuration { get; set; }

        public IEnumerable<ContactViewModel> Contacts
        {
            get
            {
                if (_contactsReadOnly == null)
                {
                    LoadContacts();
                }
                return _contactsReadOnly;
            }
        }

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

        void LoadContacts()
        {
            IsBusy = true;
            ContactRepository.LoadEntitiesAsync(result =>
                {
                    _contactsSource = new BindableCollection<ContactViewModel>(result.Data.Select(x => new ContactViewModel(x)));
                    _contactsReadOnly = new ReadOnlyObservableCollection<ContactViewModel>(_contactsSource);
                    NotifyOfPropertyChange(() => Contacts);
                    IsBusy = false;
                });
        }
    }
}
