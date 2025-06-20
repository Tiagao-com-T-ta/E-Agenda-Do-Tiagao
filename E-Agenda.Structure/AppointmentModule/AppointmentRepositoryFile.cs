using E_Agenda.Domain.AppointmentModule;
using E_Agenda.Structure.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.Structure.AppointmentModule
{
    public class AppointmentRepositoryFile : BaseRepositoryFile<Appointment>, IAppointmentRepository
    {
        public AppointmentRepositoryFile(DataContext dataContext) : base(dataContext) { }
        protected override List<Appointment> GetRegisters()
        {
            return dataContext.Appointments;
        }
    }
    
    
}
