using System.Collections.Generic;

namespace LiteApp.Bizcard.Models
{
    public static class ModelExtensions
    {
        public static Contact Copy(this Contact contact)
        {
            Contact clone = new Contact();
            clone.Id = contact.Id;
            clone.Name = contact.Name;
            clone.Organization = contact.Organization;
            clone.Email = contact.Email;
            clone.JobTitle = contact.JobTitle;
            if (contact.Phones != null)
            {
                clone.Phones = new List<Phone>(contact.Phones.Count);
                contact.Phones.ForEach(x => clone.Phones.Add(x.Copy()));
            }
            if (contact.Addresses != null)
            {
                clone.Addresses = new List<Address>(contact.Addresses.Count);
                contact.Addresses.ForEach(x => clone.Addresses.Add(x.Copy()));
            }
            return clone;
        }

        public static Address Copy(this Address address)
        {
            Address clone = new Address();
            clone.AddressDetail = address.AddressDetail;
            clone.Type = address.Type;
            return clone;
        }

        public static Phone Copy(this Phone phone)
        {
            Phone clone = new Phone();
            clone.Number = phone.Number;
            clone.Type = phone.Type;
            return clone;
        }
    }
}
