using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using LiteApp.Bizcard.Models;

namespace LiteApp.Bizcard.ViewModels
{
    public class GroupViewModel : EditableViewModel
    {
        readonly Group _group;

        public GroupViewModel(Group model)
        {
            _group = model;
        }

        public Group Model { get { return _group; } }

        public string Name
        {
            get { return _group.Name; }
            set
            {
                if (_group.Name != value)
                {
                    _group.Name = value;
                    IsDirty = true;
                    NotifyOfPropertyChange(() => Name);
                }
            }
        }
    }
}
