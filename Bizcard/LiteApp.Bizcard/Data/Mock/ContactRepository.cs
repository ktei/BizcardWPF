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
        static int _id = 1;
        List<Contact> _contacts;

        public ContactRepository()
        {
            _contacts = new List<Contact>();
            for (int i = 0; i < 10; ++i)
            {                
                _contacts.Add(MockContact());
            }
        }

        public IEnumerable<Contact> GetContacts()
        {
            foreach (var contact in _contacts)
            {
                yield return contact.Copy();
            }
        }

        public void DeleteContacts(IEnumerable<int> ids)
        {
            foreach (var id in ids)
            {
                _contacts.RemoveAll(x => x.Id == id);
            }
        }

        public Contact FindById(int id)
        {
            var contact = _contacts.FirstOrDefault(x => x.Id == id);
            if (contact != null)
            {
                return contact.Copy();
            }
            return null;
        }

        public void Save(Contact contact)
        {
            if (contact.Id == 0)
            {
                contact.Id = _id++;
                // New contact, just add it
                _contacts.Add(contact.Copy());
            }
            else
            {
                var src = _contacts.FirstOrDefault(x => x.Id == contact.Id);
                if (src != null)
                {
                    _contacts.Remove(src);
                    _contacts.Add(contact.Copy());
                }
            }
        }

        static Contact MockContact()
        {
            Contact mock = new Contact();
            mock.Id = _id++;
            mock.Name = "Name " + mock.Id;
            mock.Organization = "Organization " + mock.Id;
            mock.Email = "email@15" + mock.Id + ".com";
            mock.JobTitle = "Job " + mock.Id;
            if (mock.Id % 2 == 0)
            {
                mock.Phones = new List<Phone>() { new Phone() { Number = "1234567", Type = PhoneType.Work } };
                mock.Addresses = new List<Address>() { new Address { AddressDetail = "Address 1234567", Type = AddressType.Home } };
            }
            else
            {
                mock.Phones = new List<Phone>() { new Phone() { Number = "1234567", Type = PhoneType.Work }, 
                    new Phone() { Number = "1636236236", Type = PhoneType.Mobile } };
                mock.Addresses = new List<Address>() { new Address { AddressDetail = "Address 1234567", Type = AddressType.Home }, 
                    new Address { AddressDetail = "Address 136326236", Type = AddressType.Work } };
            }
            return mock;
        }
    }
}
