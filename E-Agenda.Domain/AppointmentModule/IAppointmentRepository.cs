﻿using E_Agenda.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.Domain.AppointmentModule
{
    public interface IAppointmentRepository : IRepository<Appointment>;
  
}
