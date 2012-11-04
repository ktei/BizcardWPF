using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using LiteApp.Bizcard.Models;

namespace LiteApp.Bizcard.ViewModels
{
    public class ContactViewModel : PropertyChangedBase
    {
        Contact _contact;
        ContactState _state = ContactState.Display;

        public ContactViewModel(Contact contact)
        {
            _contact = contact;
        }

        public string Name
        {
            get { return _contact.Name; }
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

        public void ChangeState(string state)
        {
            if (state == "Display")
            {
                State = ContactState.Display;
            }
            else
            {
                State = ContactState.Edit;
            }
        }

    }
}
