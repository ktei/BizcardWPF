using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteApp.Bizcard.Models;

namespace LiteApp.Bizcard.Data
{
    public interface IGroupRepository : IRepository
    {
        IEnumerable<Group> GetGroups();
        void DeleteGroup(int id);
        void Save(Group group);
    }
}
