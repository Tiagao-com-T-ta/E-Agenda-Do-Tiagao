using E_Agenda.Domain.ContactModule;
using E_Agenda.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace E_Agenda.WebApp.Models
{
    public abstract class ContactFormViewModel
    {
        [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
        [MinLength(2, ErrorMessage = "O campo \"Nome\" precisa conter ao menos 2 caracteres.")]
        [MaxLength(100, ErrorMessage = "O campo \"Nome\" precisa conter no máximo 100 caracteres.") ]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "O campo \"Email\" é obrigatório.")]
        [DataType (DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
        [RegularExpression(@"^\(?\d{2}\)?[\s-]?\d{4,5}[-]?\d{4}$")]
        public string Telephone { get; set; }

        public string? Role { get; set; }
        public string? Company { get; set; }
    }

    public class RegisterContactViewModel : ContactFormViewModel
    {
        public RegisterContactViewModel() { }
        public RegisterContactViewModel(string name, string email, string telephone, string? role, string? company)
        {
            Name = name;
            Email = email;
            Telephone = telephone;
            Role = role;
            Company = company;
        }

    }

    public class EditContactViewModel : ContactFormViewModel
    {
        public Guid Id { get; set; }
        public EditContactViewModel() { }
        public EditContactViewModel(Guid id, string name, string email, string telephone, string? role, string? company) : this()
        {
            Id = id;
            Name = name;
            Email = email;
            Telephone = telephone;
            Role = role;
            Company = company;
        }
    }

    public class DeleteContactViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DeleteContactViewModel(Guid id, string name) 
        {
            Id = id;
            Name = name;
        }
    }

    public class ViewContactsViewModel
    {
       public List<ContactDetailsViewModel> Contacts { get; set; }
        public ViewContactsViewModel(List<Contact> contacts)
        {
            Contacts = new List<ContactDetailsViewModel>();

            foreach (var contact in contacts)
            {
                Contacts.Add(contact.ToDetailsVM());
            }
        }
        
    }

    public class ContactDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Role { get; set; }
        public string Company { get; set; }
        
        public ContactDetailsViewModel(Guid id, string name, string email, string telephone, string? role, string? company)
        {
            Id = id;
            Name = name;
            Email = email;
            Telephone = telephone;
            Role = role;
            Company = company;
        }
    }

    public class SelectContactViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        

        public SelectContactViewModel(Guid id, string name)
        {
            Id = id;
            Name = name;
            
        }
    }
}