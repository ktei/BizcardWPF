using System;
using System.Collections.Generic;
using Caliburn.Micro;
using LiteApp.Bizcard.Helpers;
using LiteApp.Bizcard.Models;

namespace LiteApp.Bizcard.ViewModels
{
    public class AddressViewModel : PropertyChangedBase
    {
        readonly Address _address;
        readonly ContactViewModel _contact;
        static List<Tuple<string, AddressType>> _addressTypeOptions;

        public AddressViewModel(Address model, ContactViewModel contact)
        {
            _address = model;
            _contact = contact;
        }

        public Address Model { get { return _address; } }

        public AddressType Type
        {
            get { return _address.Type; }
            set
            {
                if (_address.Type != value)
                {
                    _address.Type = value;
                    _contact.IsDirty = true;
                    NotifyOfPropertyChange(() => Type);
                }
            }
        }

        public string Address
        {
            get { return _address.AddressDetail; }
            set
            {
                if (_address.AddressDetail != value)
                {
                    _address.AddressDetail = value;
                    _contact.IsDirty = true;
                    NotifyOfPropertyChange(() => Address);
                }
            }
        }

        public IEnumerable<Tuple<string, AddressType>> AddressTypeOptions
        {
            get
            {
                if (_addressTypeOptions == null)
                {
                    _addressTypeOptions = new List<Tuple<string, AddressType>>();
                    foreach (AddressType type in Enum.GetValues(typeof(AddressType)))
                    {
                        _addressTypeOptions.Add(new Tuple<string, AddressType>(type.GetDescription(), type));
                    }
                }
                return _addressTypeOptions;
            }
        }
    }
}
