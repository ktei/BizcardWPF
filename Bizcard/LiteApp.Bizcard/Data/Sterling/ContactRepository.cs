using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using LiteApp.Bizcard.Framework;
using LiteApp.Bizcard.Models;

namespace LiteApp.Bizcard.Data.Sterling
{
    [Export(typeof(IRepository))]
    [ExportMetadata("DataSource", DataSource.Sterling)]
    [ExportMetadata("Contract", typeof(IContactRepository))]
    public class ContactRepository : IContactRepository
    {
        public Models.Contact FindById(int id)
        {
            var item = SterlingService.Current.Database.Query<Contact, int>().FirstOrDefault(x => x.Key == id);
            if (item != null)
            {
                return item.LazyValue.Value;
            }
            return null;
        }

        public IEnumerable<Contact> GetContacts()
        {
            return SterlingService.Current.Database.Query<Contact, int>().Select(x => x.LazyValue.Value);
        }

        public void DeleteContacts(IEnumerable<int> ids)
        {
            foreach (var id in ids)
            {
                SterlingService.Current.Database.Delete(typeof(Contact), id);
            }
            SterlingService.Current.Database.Flush();
        }

        public void Save(Models.Contact contact)
        {
            SterlingService.Current.Database.Save(contact);
            SterlingService.Current.Database.Flush();
        }
    }
}
