﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using LiteApp.Bizcard.Resources;
using System.ComponentModel.Composition;
using LiteApp.Bizcard.Data;
using LiteApp.Bizcard.Models;
using LiteApp.Bizcard.Data.Sterling;
using System.Windows.Input;
using LiteApp.Bizcard.Helpers;

namespace LiteApp.Bizcard.ViewModels
{
    [Export]
    public class ManageGroupsViewModel : Screen
    {
        IGroupRepository _groupRepository;
        BindableCollection<GroupViewModel> _groups;
        HashSet<int> _deletedGroupIds = new HashSet<int>();

        public ManageGroupsViewModel()
        {
            DisplayName = ApplicationStrings.ManageGroupsTitle;
        }

        public event EventHandler RequestSave;
        public event EventHandler RequestRollback;

        [Import]
        public RepositoryFactory RepositoryFactory { get; set; }

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

        public IEnumerable<GroupViewModel> Items
        {
            get
            {
                return _groups;
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                return new RelayCommand(x =>
                    {
                        var item = (GroupViewModel)x;
                        _groups.Remove(item);

                        if (item.Model.Id > 0)
                        {
                            _deletedGroupIds.Add(item.Model.Id);
                        }
                    });
            }
        }

        public void Save()
        {
            if (_deletedGroupIds.Count > 0)
            {
                GroupRepository.DeleteGroups(_deletedGroupIds);
            }
            foreach (var dirtyItem in _groups.Where(x => x.IsDirty))
            {
                GroupRepository.Save(dirtyItem.Model);
                dirtyItem.IsDirty = false;
            }
            if (RequestSave != null)
                RequestSave(this, EventArgs.Empty);
        }

        public void Add()
        {
            Group g = new Group();
            var model = new GroupViewModel(g);
            model.IsDirty = true;
            _groups.Insert(0, model);
        }

        public void Rollback()
        {
            _deletedGroupIds.Clear();
            LoadGroups();
            if (RequestRollback != null)
                RequestRollback(this, EventArgs.Empty);
        }

        protected override void OnInitialize()
        {
            LoadGroups();
        }

        void LoadGroups()
        {
            var data = GroupRepository.GetGroups();
            _groups = new BindableCollection<GroupViewModel>();
            _groups.AddRange(data.Select(x => new GroupViewModel(x)));
        }
    }
}
