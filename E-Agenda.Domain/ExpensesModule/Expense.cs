using E_Agenda.Domain.CategoriesModule;
using E_Agenda.Domain.CategoryModule;
using E_Agenda.Domain.Shared;
using System;
using System.Collections.Generic;

namespace E_Agenda.Domain.ExpenseModule
{
    public class Expense : BaseEntity<Expense>
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<Category> Category { get; set; }

        public Expense()
        {
            Category = new List<Category>();
        }

        public Expense(string description, DateTime date, decimal amount,
                      PaymentMethod paymentMethod, List<Category> categories) : this()
        {
            Id = Guid.NewGuid();
            Description = description;
            Date = date;
            Amount = amount;
            PaymentMethod = paymentMethod;
            Category = categories ?? new List<Category>();
        }

        public override void Update(Expense editedExpense)
        {
            Description = editedExpense.Description;
            Date = editedExpense.Date;
            Amount = editedExpense.Amount;
            PaymentMethod = editedExpense.PaymentMethod;
            Category = editedExpense.Category;
        }
    }
}