using E_Agenda.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.Domain.ContactModule
{
    public class Contact : BaseEntity<Contact>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }

        public string Role { get; set; }
        public string Company { get; set; }

        public Contact() { }

        public Contact(string name, string email, string telephone, string? role, string? company)
        {
            if (role == null)
                role = "N/A";

            if (company == null)
                company = "N/A";

            Name = name;
            Email = email;
            Telephone = telephone;
            Role = role;
            Company = company;

        }

        public override void Update(Contact editedRegister)
        {
            Name = editedRegister.Name;
            Email = editedRegister.Email;
            Telephone = editedRegister.Telephone;
            Role = editedRegister.Role;
            Company = editedRegister.Company;

        }
    }
}
