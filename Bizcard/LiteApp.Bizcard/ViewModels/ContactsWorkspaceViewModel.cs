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
using System.Collections;
using LiteApp.Bizcard.Models;
using System.Windows.Data;

namespace LiteApp.Bizcard.ViewModels
{
    [Export(typeof(IWorkspace))]
    [ExportMetadata("Name", "Contacts")]
    public class ContactsWorkspaceViewModel : Conductor<PropertyChangedBase>.Collection.OneActive, IWorkspace
    {
        IContactRepository _contactRepository;
        IGroupRepository _groupRepository;
        bool _isBusy;
        bool _nothingIsSelected = true;
        bool _isDeleting;
        int _selectedGroupId = ShowAll;
        string _searchText = string.Empty;
        public static readonly int ShowAll = -1;

        [Import]
        public RepositoryFactory RepositoryFactory { get; set; }

        [Import]
        public IGlobalConfiguration Configuration { get; set; }

        [Import]
        public ManageGroupsViewModel ManageGroupsViewModel { get; set; }

        [Import]
        public Lazy<IWindowManager> WindowManager { get; set; }

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

        public IGroupRepository GroupRepository
        {
            get
            {
                if (_groupRepository == null)
                {
                    _groupRepository = RepositoryFactory.GetRepository<IGroupRepository>();
                }
                return _groupRepository;
            }
        }

        public IEnumerable<Tuple<string, int>> Groups
        {
            get
            {
                List<Tuple<string, int>> items = new List<Tuple<string, int>>(GroupRepository.GetGroupEntries());
                items.Insert(0, new Tuple<string,int>("All", ShowAll));
                return items;
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

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                if (_selectedGroupId != value)
                {
                    _selectedGroupId = value;
                    NotifyOfPropertyChange(() => SelectedGroupId);
                    Search(_searchText); // Refilter the contact list
                }
            }
        }

        public bool NothingIsSelected
        {
            get { return _nothingIsSelected; }
            set
            {
                if (_nothingIsSelected != value)
                {
                    _nothingIsSelected = value;
                    NotifyOfPropertyChange(() => NothingIsSelected);
                }
            }
        }

        public void Rollback(ContactViewModel contact)
        {
            var index = Items.IndexOf(contact);
            if (index >= 0)
            {
                Items.RemoveAt(index);
                if (contact == ActiveItem)
                    ActivateItem(null); // If removed item is active item, set active item to null to clear the view
                if (contact.Id != 0)
                {
                    // Find the original entity stored in database
                    var original = ContactRepository.FindById(contact.Id);
                    if (original != null)
                    {
                        var model = new ContactViewModel(original, this);
                        Items.Insert(index, model);
                        ActivateItem(model);
                    }
                }
            }
        }

        public void Search(string searchText)
        {   
            var collectionView = CollectionViewSource.GetDefaultView(Items);
            if (string.IsNullOrWhiteSpace(searchText))
            {
                _searchText = string.Empty;
                if (SelectedGroupId == ShowAll)
                {
                    collectionView.Filter = null;
                }
                else
                {
                    collectionView.Filter = x => ((ContactViewModel)x).GroupId == SelectedGroupId;
                }
            }
            else
            {
                _searchText = searchText;
                if (SelectedGroupId == ShowAll)
                {
                    collectionView.Filter = x =>
                        {
                            return ((ContactViewModel)x).Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0;
                        };
                }
                else
                {
                    collectionView.Filter = x =>
                    {
                        var model = (ContactViewModel)x;
                        return model.GroupId == SelectedGroupId && model.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0;
                    };
                }
            }
        }

        public void Add()
        {
            Contact newContact = new Contact() { Name = "New contact" };
            ContactViewModel model = new ContactViewModel(newContact, this);
            model.State = ContactState.Edit;
            model.IsDirty = true;
            ActivateItem(model);
        }

        public void Edit()
        {
            var contact = ActiveItem as ContactViewModel;
            if (contact != null)
            {
                contact.State = ContactState.Edit;
            }
        }

        public void Delete(IList selectedItems)
        {
            List<int> ids = new List<int>();
            foreach (ContactViewModel item in selectedItems)
            {
                ids.Add(item.Id);
            }
            ContactRepository.DeleteContacts(ids);
            Array itemsCopy = Array.CreateInstance(typeof(ContactViewModel), selectedItems.Count);
            selectedItems.CopyTo(itemsCopy, 0);
            _isDeleting = true; // Lock the list to reduce the times items get activated
            foreach (ContactViewModel item in itemsCopy)
            {
                Items.Remove(item);
            }
            _isDeleting = false;
            ActivateItem(null);
        }

        public void ManageGroups()
        {
            var saved = WindowManager.Value.ShowDialog(ManageGroupsViewModel);
            if (saved == true)
            {
                // Something is saved for groups so we need to reload the group list
                // and also refilter the people list
                NotifyOfPropertyChange(() => Groups);
                if (SelectedGroupId != -1 && !GroupRepository.Exists(SelectedGroupId))
                {
                    // Selected group has been deleted
                    // Force selecting All
                    SelectedGroupId = ShowAll;
                }
            }
        }

        public override void ActivateItem(PropertyChangedBase item)
        {
            if (_isDeleting)
                return;
            NothingIsSelected = item == null;
            base.ActivateItem(item);
        }

        protected override PropertyChangedBase DetermineNextItemToActivate(IList<PropertyChangedBase> list, int lastIndex)
        {
            // Handles the scenario where the last item is deselected so we need to return nothing
            // letting conductor that ActiveItem should be null
            return null;
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
                        Items.AddRange(data.Select(x => new ContactViewModel(x, this)));
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
