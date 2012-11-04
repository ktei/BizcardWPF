using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteApp.Bizcard.Models;
using System.Threading;
using System.ComponentModel.Composition;
using LiteApp.Bizcard.Framework;

namespace LiteApp.Bizcard.Data.Mock
{
    [Export(typeof(IRepository))]
    [ExportMetadata("DataSource", DataSource.Mock)]
    [ExportMetadata("Contract", typeof(IContactRepository))]
    public class ContactRepository : IContactRepository
    {
        List<Contact> _contacts;

        public IEnumerable<Contact> GetContacts()
        {
            if (_contacts == null)
            {
                _contacts = new List<Contact>();
                for (int i = 0; i < 10; ++i)
                {
                    _contacts.Add(new Contact() { Name = "Name " + i });
                }
            }
            return _contacts;
        }
    }
}
