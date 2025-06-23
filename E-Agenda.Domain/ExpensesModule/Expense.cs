using E_Agenda.Domain.CategoriesModule;
using E_Agenda.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.Domain.ExpensesModule
{
    public class Expense : BaseEntity<Expense>
    {
        public string Description { get; set; }

        public DateTime When { get; set; }

        public string Amount { get; set; }

        public string PaymentMethod { get; set; }

        public List<Category> Category { get; set; }


        public Expense()
        {
        }

        public Expense (string description, DateTime when, string amount, string paymentMethod, List<Category> category) : this()
        {
            Id = Guid.NewGuid();
            Description = description;
            When = when;
            Amount = amount;
            PaymentMethod = paymentMethod;
            Category = category;
            Category = new List<Category>();

        }

        public override void Update(Expense editedRegister)
        {
            Description = editedRegister.Description;
            When = editedRegister.When;
            Amount = editedRegister.Amount;
            PaymentMethod = editedRegister.PaymentMethod;
            Category = editedRegister.Category;
        }
    }
}
