using E_Agenda.Domain.ContactModule;
using E_Agenda.Structure.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.Structure.ContactModule
{
    public class ContactRepositoryFile : BaseRepositoryFile<Contact>, IContactRepository
    {
        public ContactRepositoryFile(DataContext dataContext) : base(dataContext) { }

        protected override List<Contact> GetRegisters()
        {
            return dataContext.Contacts;
        }
    }
    
    
}
