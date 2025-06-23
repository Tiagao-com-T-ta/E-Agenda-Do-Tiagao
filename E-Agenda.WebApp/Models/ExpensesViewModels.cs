using E_Agenda.Domain.CategoryModule;
using E_Agenda.Domain.ExpenseModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Agenda.WebApp.Models
{
    public abstract class ExpenseViewModel
    {
        [Required(ErrorMessage = "Description is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Description must be between 2 and 100 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Payment method is required")]
        public PaymentMethod PaymentMethod { get; set; }

        [Required(ErrorMessage = "At least one category is required")]
        public List<Category> SelectedCategories { get; set; } = new List<Category>();
    }

    public class RegisterExpenseViewModel : ExpenseViewModel
    {
        public RegisterExpenseViewModel() { }
    }

    public class EditExpenseViewModel : ExpenseViewModel
    {
        public Guid Id { get; set; }
    }

    public class ExpenseDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string FormattedAmount => Amount.ToString("C");
        public PaymentMethod PaymentMethod { get; set; }
        public List<Category> Categories { get; set; }

        public ExpenseDetailsViewModel(Expense expense)
        {
            Id = expense.Id;
            Description = expense.Description;
            Date = expense.Date;
            Amount = expense.Amount;
            PaymentMethod = expense.PaymentMethod;
            Categories = expense.Category;
        }
    }

    public class ExpenseListViewModel
    {
        public List<ExpenseDetailsViewModel> Expenses { get; set; }

        public ExpenseListViewModel(List<Expense> expenses)
        {
            Expenses = expenses.ConvertAll(e => new ExpenseDetailsViewModel(e));
        }
    }
}
