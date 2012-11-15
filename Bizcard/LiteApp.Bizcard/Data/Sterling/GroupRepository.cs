using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteApp.Bizcard.Models;
using System.ComponentModel.Composition;
using LiteApp.Bizcard.Framework;

namespace LiteApp.Bizcard.Data.Sterling
{
    [Export(typeof(IRepository))]
    [ExportMetadata("DataSource", DataSource.Sterling)]
    [ExportMetadata("Contract", typeof(IGroupRepository))]
    public class GroupRepository : IGroupRepository
    {
        public IEnumerable<Group> GetGroups()
        {
            return SterlingService.Current.Database.Query<Group, int>().Select(x => x.LazyValue.Value).ToList();
        }

        public void DeleteGroups(IEnumerable<int> ids)
        {
            foreach (var id in ids)
            {
                var peopleInThisGroup = SterlingService.Current.Database.Query<Contact, int>();
                foreach (var item in peopleInThisGroup)
                {
                    // Change and save his GroupdId to null to remove him from this group
                    var people = item.LazyValue.Value;
                    if (people.GroupIds != null)
                    {
                        people.GroupIds.Remove(id);
                    }
                    SterlingService.Current.Database.Save(people);
                }

                SterlingService.Current.Database.Delete(typeof(Group), id);
            }
            SterlingService.Current.Database.Flush();
        }

        public void Save(Group group)
        {
            bool isNew = group.Id == 0;
            SterlingService.Current.Database.Save(group);
            SterlingService.Current.Database.Flush();
        }

        public IEnumerable<Tuple<string, int>> GetGroupEntries()
        {
            return SterlingService.Current.Database.Query<Group, string, int>("Name").Select(x => new Tuple<string, int>(x.Index, x.Key));
        }

        public bool Exists(int id)
        {
            return SterlingService.Current.Database.Query<Group, int>().Any(x => x.Key == id);
        }
    }
}
