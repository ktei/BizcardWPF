using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteApp.Bizcard.Models;
using LiteApp.Bizcard.Helpers;
using Caliburn.Micro;

namespace LiteApp.Bizcard.ViewModels
{
    public class PhoneViewModel : PropertyChangedBase
    {
        readonly Phone _phone;
        readonly ContactViewModel _contact;
        static List<Tuple<string, PhoneType>> _phoneTypeOptions;

        public PhoneViewModel(Phone model, ContactViewModel contact)
        {
            _phone = model;
            _contact = contact;
        }

        public Phone Model
        {
            get { return _phone; }
        }

        public PhoneType Type
        {
            get { return _phone.Type; }
            set
            {
                if (_phone.Type != value)
                {
                    _phone.Type = value;
                    _contact.IsDirty = true;
                    NotifyOfPropertyChange(() => Type);
                }
            }
        }

        public string Number
        {
            get { return _phone.Number; }
            set
            {
                if (_phone.Number != value)
                {
                    _phone.Number = value;
                    _contact.IsDirty = true;
                    NotifyOfPropertyChange(() => Number);
                }
            }
        }

        public IEnumerable<Tuple<string, PhoneType>> PhoneTypeOptions
        {
            get
            {
                if (_phoneTypeOptions == null)
                {
                    _phoneTypeOptions = new List<Tuple<string,PhoneType>>();
                    foreach (PhoneType type in Enum.GetValues(typeof(PhoneType)))
                    {
                        _phoneTypeOptions.Add(new Tuple<string, PhoneType>(type.GetDescription(), type));
                    }
                }
                return _phoneTypeOptions;
            }
        }
    }
}
