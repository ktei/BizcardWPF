using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using LiteApp.Bizcard.Models;
using System.ComponentModel;

namespace LiteApp.Bizcard.ViewModels
{
    public class GroupViewModel : EditableViewModel//, IDataErrorInfo
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

        //public string Error
        //{
        //    get
        //    {
        //        return string.Empty;
        //    }
        //}

        //public string this[string propertyName]
        //{
        //    get
        //    {
        //        if (propertyName == "Name")
        //        {
        //            if (string.IsNullOrWhiteSpace(Name))
        //            {
        //                return "Name cannot be blank.";
        //            }
        //        }
        //        return string.Empty;
        //    }
        //}
    }
}
