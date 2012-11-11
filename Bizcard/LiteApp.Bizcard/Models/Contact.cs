using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteApp.Bizcard.Models
{
    public class Contact : BaseModel<Contact>
    {
        public string Name { get; set; }

        public string Organization { get; set; }

        public string JobTitle { get; set; }

        public string Email { get; set; }

        public List<Phone> Phones { get; set; }

        public List<Address> Addresses { get; set; }
    }
}
