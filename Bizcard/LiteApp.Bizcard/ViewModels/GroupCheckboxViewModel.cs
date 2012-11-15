using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace LiteApp.Bizcard.ViewModels
{
    public class GroupCheckboxViewModel : PropertyChangedBase
    {
        bool _isChecked;
        readonly ContactViewModel _contact;

        public GroupCheckboxViewModel(string name, int groupId, ContactViewModel contact)
        {
            Name = name;
            GroupId = groupId;
            _contact = contact;
        }

        public string Name { get; private set; }

        public int GroupId { get; private set; }

        public void Check()
        {
            _isChecked = true;
        }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    _contact.IsDirty = true;
                    NotifyOfPropertyChange(() => IsChecked);
                }
            }
        }
    }
}
