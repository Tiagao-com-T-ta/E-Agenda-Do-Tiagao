using E_Agenda.Domain.ContactModule;
using E_Agenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Agenda.WebApp.Extensions
{
    public static class ContactExtensions 
    {
        public static Contact ToEntity(this ContactFormViewModel form)
        {
            return new Contact(form.Name, form.Email, form.Telephone, form.Role, form.Company);
        }

        public static ContactDetailsViewModel ToDetailsVM(this Contact contact)
        {
            return new ContactDetailsViewModel(

                contact.Id,
                contact.Name,
                contact.Email,
                contact.Telephone,
                contact.Role,
                contact.Company
            );
        }
    }
}
