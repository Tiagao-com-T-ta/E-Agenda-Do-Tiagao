using E_Agenda.Domain.AppointmentModule;
using E_Agenda.Domain.ContactModule;
using E_Agenda.Domain.CategoriesModule;
using E_Agenda.Domain.ExpensesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaskManagement.Domain.TaskModule;

namespace E_Agenda.Structure.Shared
{
    public class DataContext
    {
        private string folder = "C:\\temp";
        private string file = "E-Agenda-Data.json";

        public List<Category> Categories { get; internal set; }
        public List<Expense> Expenses { get; internal set; }
        public List<TasksClass> TasksClass { get; set;  }
        public List<Contact> Contacts { get; set; }
        public List<Appointment> Appointments { get; set; }

        public DataContext()
        {
            TasksClass = new List<TasksClass>();
            Contacts = new List<Contact>();
            Appointments = new List<Appointment>();
            Categories = new List<Category>();
            Expenses = new List<Expense>();
        }

        public DataContext(bool loadData) : this()
        {
            if (loadData)
            {
                LoadData();
            }
        }


        public void SaveData()
        {
            string fullPath = Path.Combine(folder, file);

            JsonSerializerOptions jsonOptions = new JsonSerializerOptions();


            jsonOptions.WriteIndented = true;
            jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

            string json = JsonSerializer.Serialize(this, jsonOptions);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            File.WriteAllText(fullPath, json);

        }  
        
        public void LoadData()
        {
            string fullPath = Path.Combine(folder, file);
            
            if (!File.Exists(fullPath)) return;

            string json = File.ReadAllText(fullPath);
            
            if (string.IsNullOrWhiteSpace(json)) return;

            JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
            jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

            DataContext? dataContext = JsonSerializer.Deserialize<DataContext>(json, jsonOptions)!;
            
            if (dataContext == null) return;

            Categories = dataContext.Categories;
            Expenses = dataContext.Expenses;
            TasksClass = dataContext.TasksClass;
            Contacts = dataContext.Contacts;
            Appointments = dataContext.Appointments;
        }
    }
}
