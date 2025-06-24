using E_Agenda.Domain.Shared;
using E_Agenda.Domain.CategoriesModule;
using System;
using System.Collections.Generic;
using E_Agenda.Domain.CategoryModule;

namespace E_Agenda.Domain.ExpenseModule
{
    public class Expense : BaseEntity<Expense>
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime OccurrenceDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<Category> Categories { get; set; }

        public Expense()
        {
            Categories = new List<Category>();
        }

        public Expense(string description, decimal value, DateTime occurrenceDate, PaymentMethod paymentMethod) : this()
        {
            Id = Guid.NewGuid();
            Description = description;
            Value = value;
            OccurrenceDate = occurrenceDate;
            PaymentMethod = paymentMethod;
        }

        public void RegisterCategory(Category category)
        {
            if (Categories.Contains(category))
                return;

            category.Expenses.Add(this);
            Categories.Add(category);
        }

        public void RemoveCategory(Category category)
        {
            if (!Categories.Contains(category))
                return;

            category.Expenses.Remove(this);
            Categories.Remove(category);
        }

        public override void Update(Expense editedExpense)
        {
            Description = editedExpense.Description;
            Value = editedExpense.Value;
            OccurrenceDate = editedExpense.OccurrenceDate;
            PaymentMethod = editedExpense.PaymentMethod;
        }
    }
}
