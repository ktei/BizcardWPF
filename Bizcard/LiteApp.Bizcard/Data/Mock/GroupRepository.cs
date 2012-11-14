using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteApp.Bizcard.Models;
using System.ComponentModel.Composition;
using LiteApp.Bizcard.Framework;

namespace LiteApp.Bizcard.Data.Mock
{
    [Export(typeof(IRepository))]
    [ExportMetadata("DataSource", DataSource.Mock)]
    [ExportMetadata("Contract", typeof(IGroupRepository))]
    public class GroupRepository : IGroupRepository
    {
        public IEnumerable<Models.Group> GetGroups()
        {
            foreach (var i in Enumerable.Range(1, 10))
            {
                yield return new Group() { Id = i, Name = "Group " + i };
            }
        }

        public void DeleteGroups(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public void Save(Models.Group group)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Tuple<string, int>> GetGroupEntries()
        {
            throw new NotImplementedException();
        }


        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
