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

        public GroupCheckboxViewModel(string name, int groupId)
        {
            Name = name;
            GroupId = groupId;
        }

        public string Name { get; private set; }

        public int GroupId { get; private set; }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    NotifyOfPropertyChange(() => IsChecked);
                }
            }
        }
    }
}
