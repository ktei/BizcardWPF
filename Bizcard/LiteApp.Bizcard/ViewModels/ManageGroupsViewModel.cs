using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using LiteApp.Bizcard.Resources;
using System.ComponentModel.Composition;
using LiteApp.Bizcard.Data;

namespace LiteApp.Bizcard.ViewModels
{
    [Export]
    public class ManageGroupsViewModel : Screen
    {
        IGroupRepository _groupRepository;
        BindableCollection<GroupViewModel> _groups;

        public ManageGroupsViewModel()
        {
            DisplayName = ApplicationStrings.ManageGroupsTitle;
        }

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
