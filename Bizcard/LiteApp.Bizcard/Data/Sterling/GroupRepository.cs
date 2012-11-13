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
            return SterlingService.Current.Database.Query<Group, int>().Select(x => x.LazyValue.Value);
        }

        public void DeleteGroup(int id)
        {
            var peopleInThisGroup = SterlingService.Current.Database.Query<Contact, int?, int>("GroupId").Where(x => x.Index == id);
            foreach (var item in peopleInThisGroup)
            {
                // Change and save his GroupdId to null to remove him from this group
                var people = item.LazyValue.Value;
                people.GroupId = null;
                SterlingService.Current.Database.Save(people);
            }

            SterlingService.Current.Database.Delete(typeof(Group), id);
            SterlingService.Current.Database.Flush();
        }

        public void Save(Group group)
        {
            SterlingService.Current.Database.Save(group);
            SterlingService.Current.Database.Flush();
        }
    }
}
