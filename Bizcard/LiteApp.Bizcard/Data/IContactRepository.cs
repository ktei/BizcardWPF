using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteApp.Bizcard.Models;

namespace LiteApp.Bizcard.Data
{
    public interface IContactRepository : IRepository
    {
        Contact FindById(int id);
        IEnumerable<Contact> GetContacts();
        void DeleteContacts(IEnumerable<int> ids);
        void Save(Contact contact);
    }
}
