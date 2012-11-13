using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteApp.Bizcard.Models
{
    public class Contact : BaseModel<Contact>
    {
        public int? GroupId { get; set; }

        public string Name { get; set; }

        public string Organization { get; set; }

        public string JobTitle { get; set; }

        public string Email { get; set; }

        public string Skype { get; set; }

        public string Website { get; set; }

        public string MSN { get; set; }

        public string QQ { get; set; }

        public string Notes { get; set; }

        public List<Phone> Phones { get; set; }

        public List<Address> Addresses { get; set; }
    }
}
