using System.Collections.Generic;
using System.Windows.Input;
using Caliburn.Micro;
using LiteApp.Bizcard.Helpers;
using LiteApp.Bizcard.Models;
using System.ComponentModel.Composition;
using LiteApp.Bizcard.Data;
using System.Linq;
using System;

namespace LiteApp.Bizcard.ViewModels
{
    public class ContactViewModel : EditableViewModel
    {
        ContactState _state = ContactState.Display;
        BindableCollection<PhoneViewModel> _phones;
        BindableCollection<AddressViewModel> _addresses;
        List<GroupCheckboxViewModel> _groups;
        Contact _contact;
        readonly ContactsWorkspaceViewModel _contactsWorkspace;
        static IContactRepository _contactRepository;
        static IGroupRepository _groupRepository;
        ContactInfoCategory _selectedInfoCategory = ContactInfoCategory.Primary;
        bool _hasGroups;

        public ContactViewModel(Contact model, ContactsWorkspaceViewModel workspace)
        {
            _contact = model;
            _contactsWorkspace = workspace;
        }

        [Import]
        public RepositoryFactory RepositoryFactory { get; set; }

        public IContactRepository ContactRepository
        {
            get
            {
                if (_contactRepository == null)
                {
                    this.SatisfyImports();
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
                    this.SatisfyImports();
                    _groupRepository = RepositoryFactory.GetRepository<IGroupRepository>();
                }
                return _groupRepository;
            }
        }
        
        public int Id
        {
            get { return _contact.Id; }
        }

        public List<int> GroupIds
        {
            get { return _contact.GroupIds ?? new List<int>(0); }
        }

        public List<GroupCheckboxViewModel> Groups
        {
            get
            {
                LoadGroups();
                return _groups;
            }
        }

        public bool HasGroups
        {
            get { return _hasGroups; }
            private set
            {
                if (_hasGroups != value)
                {
                    _hasGroups = value;
                    NotifyOfPropertyChange(() => HasGroups);
                }
            }
        }

        public ContactInfoCategory SelectedInfoCategory
        {
            get { return _selectedInfoCategory; }
            set
            {
                if (_selectedInfoCategory != value)
                {
                    _selectedInfoCategory = value;
                    NotifyOfPropertyChange(() => SelectedInfoCategory);
                }
            }
        }

        public string Name
        {
            get { return _contact.Name; }
            set
            {
                if (_contact.Name != value)
                {
                    _contact.Name = value;
                    IsDirty = true;
                    NotifyOfPropertyChange(() => Name);
                }
            }
        }

        public string Organization
        {
            get { return _contact.Organization; }
            set
            {
                if (_contact.Organization != value)
                {
                    _contact.Organization = value;
                    IsDirty = true;
                    NotifyOfPropertyChange(() => Organization);
                }
            }
        }

        public string Email
        {
            get { return _contact.Email; }
            set
            {
                if (_contact.Email != value)
                {
                    _contact.Email = value;
                    IsDirty = true;
                    NotifyOfPropertyChange(() => Email);
                }
            }
        }

        public string JobTitle
        {
            get { return _contact.JobTitle; }
            set
            {
                if (_contact.JobTitle != value)
                {
                    _contact.JobTitle = value;
                    IsDirty = true;
                    NotifyOfPropertyChange(() => JobTitle);
                }
            }
        }

        public IEnumerable<PhoneViewModel> Phones
        {
            get
            {
                if (_phones == null)
                {
                    if (_contact.Phones == null)
                    {
                        _phones = new BindableCollection<PhoneViewModel>();
                    }
                    else
                    {
                        List<PhoneViewModel> src = new List<PhoneViewModel>(_contact.Phones.Count);
                        _contact.Phones.ForEach(x => src.Add(new PhoneViewModel(x, this)));
                        _phones = new BindableCollection<PhoneViewModel>(src);
                    }
                }
                return _phones;
            }
        }

        public IEnumerable<AddressViewModel> Addresses
        {
            get
            {
                if (_addresses == null)
                {
                    if (_contact.Addresses == null)
                    {
                        _addresses = new BindableCollection<AddressViewModel>();
                    }
                    else
                    {
                        List<AddressViewModel> src = new List<AddressViewModel>(_contact.Addresses.Count);
                        _contact.Addresses.ForEach(x => src.Add(new AddressViewModel(x, this)));
                        _addresses = new BindableCollection<AddressViewModel>(src);
                    }
                }
                return _addresses;
            }
        }

        public string Skype
        {
            get { return _contact.Skype; }
            set
            {
                if (_contact.Skype != value)
                {
                    _contact.Skype = value;
                    IsDirty = true;
                    NotifyOfPropertyChange(() => Skype);
                }
            }
        }

        public string Website
        {
            get { return _contact.Website; }
            set
            {
                if (_contact.Website != value)
                {
                    _contact.Website = value;
                    IsDirty = true;
                    NotifyOfPropertyChange(() => Website);
                }
            }
        }

        public string MSN
        {
            get { return _contact.MSN; }
            set
            {
                if (_contact.MSN != value)
                {
                    _contact.MSN = value;
                    IsDirty = true;
                    NotifyOfPropertyChange(() => MSN);
                }
            }
        }

        public string QQ
        {
            get { return _contact.QQ; }
            set
            {
                if (_contact.QQ != value)
                {
                    _contact.QQ = value;
                    IsDirty = true;
                    NotifyOfPropertyChange(() => QQ);
                }
            }
        }

        public string Notes
        {
            get { return _contact.Notes; }
            set
            {
                if (_contact.Notes != value)
                {
                    _contact.Notes = value;
                    IsDirty = true;
                    NotifyOfPropertyChange(() => Notes);
                }
            }
        }

        public ContactState State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    NotifyOfPropertyChange(() => State);
                }
            }
        }

        public ICommand AddPhoneCommand
        {
            get
            {
                return new RelayCommand(x =>
                    {
                        Phone phone = new Phone();
                        var model = new PhoneViewModel(phone, this);
                        _phones.Add(model);
                        IsDirty = true;
                    });
            }
        }

        public ICommand RemovePhoneCommand
        {
            get
            {
                return new RelayCommand(x =>
                    {
                        _phones.Remove((PhoneViewModel)x);
                        IsDirty = true;
                    });
            }
        }

        public ICommand AddAddressCommand
        {
            get
            {
                return new RelayCommand(x =>
                    {
                        Address address = new Address();
                        var model = new AddressViewModel(address, this);
                        _addresses.Add(model);
                        IsDirty = true;
                    });
            }
        }

        public ICommand RemoveAddressCommand
        {
            get
            {
                return new RelayCommand(x =>
                    {
                        _addresses.Remove((AddressViewModel)x);
                        IsDirty = true;
                    });
            }
        }

        public void Edit()
        {
            State = ContactState.Edit;
        }
        
        public void Rollback()
        {
            _contactsWorkspace.Rollback(this);
        }

        public void Save()
        {
            if (IsDirty)
            {
                PrepareSave();
                ContactRepository.Save(_contact);
                IsDirty = false;
            }
            State = ContactState.Display;
            SelectedInfoCategory = ContactInfoCategory.Primary;
        }

        void PrepareSave()
        {
            _contact.Phones = new List<Phone>(_phones.Select(x => x.Model));
            _contact.Addresses = new List<Address>(_addresses.Select(x => x.Model));
            _contact.GroupIds = new List<int>(_groups.Where(x => x.IsChecked).Select(x => x.GroupId));
        }

        void LoadGroups()
        {
            List<int> checkedGroupIds = new List<int>();
            if (_groups != null)
            {
                checkedGroupIds.AddRange(_groups.Where(x => x.IsChecked).Select(x => x.GroupId));
            }
            else
            {
                checkedGroupIds.AddRange(GroupIds);
            }
            _groups = new List<GroupCheckboxViewModel>(GroupRepository.GetGroupEntries()
                .Select(x => new GroupCheckboxViewModel(x.Item1, x.Item2, this)));
            foreach (var id in checkedGroupIds)
            {
                var model = _groups.FirstOrDefault(x => x.GroupId == id);
                if (model != null)
                {
                    model.Check();
                }
            }
            HasGroups = _groups.Count > 0;
        }
    }
}
