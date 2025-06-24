using E_Agenda.Domain.CategoriesModule;
using E_Agenda.Domain.CategoryModule;
using E_Agenda.Domain.ExpenseModule;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Agenda.WebApp.Models
{
    public class ExpenseFormViewModel
    {
        [Required(ErrorMessage = "The field \"Description\" is required.")]
        [MinLength(2, ErrorMessage = "The field \"Description\" must have at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "The field \"Description\" must have at most 100 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "The field \"Occurrence Date\" is required.")]
        public DateTime OccurrenceDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "The field \"Value\" is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The field \"Value\" must be a positive number.")]
        public decimal Value { get; set; } = 0m;

        [Required(ErrorMessage = "The field \"Payment Method\" is required.")]
        public PaymentMethod PaymentMethod { get; set; }

        [Required(ErrorMessage = "You must select at least one category.")]
        public List<Guid>? SelectedCategories { get; set; }

        public List<SelectListItem>? AvailableCategories { get; set; }
    }

    public class RegisterExpenseViewModel : ExpenseFormViewModel
    {
        public RegisterExpenseViewModel()
        {
            SelectedCategories = new List<Guid>();
            AvailableCategories = new List<SelectListItem>();
        }

        public RegisterExpenseViewModel(List<Category> availableCategories) : this()
        {
            foreach (var c in availableCategories)
            {
                var selectItem = new SelectListItem(c.Title, c.Id.ToString());
                AvailableCategories.Add(selectItem);
            }
        }
    }

    public class EditExpenseViewModel : ExpenseFormViewModel
    {
        public Guid Id { get; set; }

        public EditExpenseViewModel()
        {
            SelectedCategories = new List<Guid>();
            AvailableCategories = new List<SelectListItem>();
        }

        public EditExpenseViewModel(
            Guid id,
            string description,
            decimal value,
            DateTime occurrenceDate,
            PaymentMethod paymentMethod,
            List<Category> selectedCategories,
            List<Category> availableCategories
        ) : this()
        {
            Id = id;
            Description = description;
            Value = value;
            OccurrenceDate = occurrenceDate;
            PaymentMethod = paymentMethod;

            foreach (var cat in selectedCategories)
                SelectedCategories!.Add(cat.Id);

            foreach (var c in availableCategories)
                AvailableCategories!.Add(new SelectListItem(c.Title, c.Id.ToString()));
        }
    }

    public class DeleteExpenseViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public DeleteExpenseViewModel(Guid id, string description)
        {
            Id = id;
            Description = description;
        }
    }

    public class VisualizeExpensesViewModel
    {
        public List<ExpenseDetailsViewModel> Records { get; set; }

        public VisualizeExpensesViewModel(List<Expense> expenses)
        {
            Records = new List<ExpenseDetailsViewModel>();

            foreach (var e in expenses)
                Records.Add(new ExpenseDetailsViewModel(
                    e.Id,
                    e.Description,
                    e.Value,
                    e.OccurrenceDate,
                    e.PaymentMethod,
                    e.Categories
                ));
        }
    }

    public class ExpenseDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime OccurrenceDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<string> Categories { get; set; }

        public ExpenseDetailsViewModel(
            Guid id,
            string description,
            decimal value,
            DateTime occurrenceDate,
            PaymentMethod paymentMethod,
            List<Category> categories
        )
        {
            Id = id;
            Description = description;
            Value = value;
            OccurrenceDate = occurrenceDate;
            PaymentMethod = paymentMethod;
            Categories = new List<string>();

            foreach (var c in categories)
                Categories.Add(c.Title);
        }
    }
}
