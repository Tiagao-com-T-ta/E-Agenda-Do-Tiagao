using E_Agenda.Domain.ContactModule;
using E_Agenda.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.Domain.AppointmentModule
{
    public class Appointment : BaseEntity<Appointment>
    {
        public string Topic { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Type { get; set; }
        public string? Link { get; set; } = "-/-";
        public string? Location { get; set; } = "-/-";


        public Appointment() { }

        public Appointment(string topic, DateOnly date, TimeOnly startTime, TimeOnly endTime, string type, string? link, string? location)
        {
            if (string.IsNullOrWhiteSpace(link))
                link = "-/-";

            if (string.IsNullOrWhiteSpace(location))
                location = "-/-";

            Id = Guid.NewGuid();
            Topic = topic;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Type = type;
            Link = link;
            Location = location;
        }

        public override void Update(Appointment editedRegister)
        {
            Topic = editedRegister.Topic;
            Date = editedRegister.Date;
            StartTime = editedRegister.StartTime;
            EndTime = editedRegister.EndTime;
            Type = editedRegister.Type;
            Link = editedRegister.Link;
            Location = editedRegister.Location;
        }
    }
}
