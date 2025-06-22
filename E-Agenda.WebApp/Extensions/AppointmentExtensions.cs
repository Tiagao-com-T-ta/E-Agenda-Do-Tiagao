using E_Agenda.Domain.AppointmentModule;
using E_Agenda.WebApp.Models;

namespace E_Agenda.WebApp.Extensions
{
    public static class AppointmentExtensions
    {
        public static Appointment ToEntity(this AppointmentFormViewModel form)
        {
            return new Appointment(
                form.Topic,
                form.Date,
                form.StartTime,
                form.EndTime,
                form.Type,
                form.Link,
                form.Location,
                form.ContactId
            );
        }

        public static AppointmentDetailsViewModel ToDetailsVM(this Appointment appointment)
        {
            return new AppointmentDetailsViewModel(
                appointment.Id,
                appointment.Topic,
                appointment.Date,
                appointment.StartTime,
                appointment.EndTime,
                appointment.Type,
                appointment.Link,
                appointment.Location,
                appointment.ContactId,
                appointment.Contact?.Name ?? "Nenhum"
            );
        }
    }
}
