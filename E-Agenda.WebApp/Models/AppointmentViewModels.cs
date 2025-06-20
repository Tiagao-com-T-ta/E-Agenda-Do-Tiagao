using E_Agenda.Domain.AppointmentModule;
using E_Agenda.Domain.ContactModule;
using E_Agenda.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace E_Agenda.WebApp.Models
{
    public abstract class AppointmentFormViewModels
    {
        [Required(ErrorMessage = "O campo \"Assunto\" é obrigatório.")]
        [MinLength(2, ErrorMessage = "O campo \"Assunto\" precisa conter ao menos 2 caracteres.")]
        [MaxLength(100, ErrorMessage = "O campo \"Assunto\" precisa conter no máximo 100 caracteres.")]
        public string Topic { get; set; }

        [Required(ErrorMessage = "O campo \"Data\" é obrigatório.")]
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }

        [Required(ErrorMessage = "O campo \"Hora de Início\" é obrigatório.")]
        [DataType(DataType.Time)]
        public TimeOnly StartTime { get; set; }

        [Required(ErrorMessage = "O campo \"Hora de Termino\" é obrigatório.")]
        [DataType(DataType.Time)]
        public TimeOnly EndTime { get; set; }

        public string Type { get; set; }
        public string? Link { get; set; }
        public string? Location { get; set; }

        public Contact Contact { get; set; }
    }

    public class RegisterAppointmentViewModel : AppointmentFormViewModels
    {
        public RegisterAppointmentViewModel() { }
        public RegisterAppointmentViewModel(string topic, DateOnly date, TimeOnly startTime, TimeOnly endTime, string type, string? link, string? location, Contact contact)
        {
            Topic = topic;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Type = type;
            Link = link;
            Location = location;
            Contact = contact;
        }
    }

    public class EditAppointmentViewModel : AppointmentFormViewModels
    {
        public Guid Id { get; set; }
        public EditAppointmentViewModel() { }
        public EditAppointmentViewModel(Guid id, string topic, DateOnly date, TimeOnly startTime, TimeOnly endTime, string type, string? link, string? location, Contact contact) : this()
        {
            Id = id;
            Topic = topic;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Type = type;
            Link = link;
            Location = location;
            Contact = contact;
        }
    }

    public class DeleteAppointmentViewModel
    {
        public Guid Id { get; set; }
        public string Topic { get; set; }
        public DeleteAppointmentViewModel(Guid id, string topic)
        {
            Id = id;
            Topic = topic;
        }
    }

    public class ViewAppointmentsViewModel
    {
        public List<AppointmentDetailsViewModel> Appointments { get; set; }
        public ViewAppointmentsViewModel(List<Appointment> appointments)
        {
            Appointments = new List<AppointmentDetailsViewModel>();

            foreach (var appointment in appointments)
            {
                Appointments.Add(appointment.ToDetailsVM());
            }
        }
    }

    public class AppointmentDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Topic { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Type { get; set; }
        public string? Link { get; set; }
        public string? Location { get; set; }
        public Contact? Contact { get; set; }

        public AppointmentDetailsViewModel() { }
        public AppointmentDetailsViewModel(Guid id, string topic, DateOnly date, TimeOnly startTime, TimeOnly endTime, string type, string? location, string? link, Contact? contact)
        {
            Id = id;
            Topic = topic;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Type = type;
            Location = location;
            Link = link;
            Contact = contact;
        }
    }

    public class SelectAppointmentsViewModel
    {
        public Guid Id { get; set; }
        public string Topic { get; set; }

        public SelectAppointmentsViewModel(Guid id, string topic)
        {
            Id = id;
            Topic = topic;
        }
    }
}
