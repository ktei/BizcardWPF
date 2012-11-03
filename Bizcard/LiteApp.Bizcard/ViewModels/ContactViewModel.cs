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

        public ContactViewModel(Contact contact)
        {
            _contact = contact;
        }

        public string Name
        {
            get { return _contact.Name; }
        }
    }
}
