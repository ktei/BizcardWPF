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
        List<Group> _entities;

        public IEnumerable<Group> GetGroups()
        {
            //if (_entities == null)
            //{
            //    // Cache the loaded entities
            //    _entities = SterlingService.Current.Database.Query<Group, int>().Select(x => x.LazyValue.Value).ToList();
            //}
            //return _entities;
            return SterlingService.Current.Database.Query<Group, int>().Select(x => x.LazyValue.Value).ToList();
        }

        public void DeleteGroups(IEnumerable<int> ids)
        {
            foreach (var id in ids)
            {
                var peopleInThisGroup = SterlingService.Current.Database.Query<Contact, int, int>("GroupId").Where(x => x.Index == id);
                foreach (var item in peopleInThisGroup)
                {
                    // Change and save his GroupdId to null to remove him from this group
                    var people = item.LazyValue.Value;
                    people.GroupIds = null;
                    SterlingService.Current.Database.Save(people);
                }

                SterlingService.Current.Database.Delete(typeof(Group), id);
            }
            SterlingService.Current.Database.Flush();
            //foreach (var id in ids)
            //{
            //    _entities.RemoveAll(x => x.Id == id); // Remove it from cache
            //}
        }

        public void Save(Group group)
        {
            bool isNew = group.Id == 0;
            SterlingService.Current.Database.Save(group);
            SterlingService.Current.Database.Flush();
            //if (isNew)
            //{
            //    _entities.Add(group);
            //}
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
