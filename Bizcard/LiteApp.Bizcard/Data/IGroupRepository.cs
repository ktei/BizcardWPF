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
        IEnumerable<Tuple<string, int>> GetGroupEntries();
        void DeleteGroups(IEnumerable<int> ids);
        bool Exists(int id);
        void Save(Group group);
    }
}
