using E_Agenda.Domain.CategoriesModule;
using E_Agenda.Domain.CategoryModule;
using E_Agenda.Domain.ExpenseModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Agenda.WebApp.Models
{
    public class CategoryFormViewModel
    {
        [Required(ErrorMessage = "The field \"Title\" is required.")]
        [MinLength(2, ErrorMessage = "The field \"Title\" must have at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "The field \"Title\" must have at most 100 characters.")]
        public string? Title { get; set; }
    }

    public class RegisterCategoryViewModel : CategoryFormViewModel
    {
        public RegisterCategoryViewModel() { }

        public RegisterCategoryViewModel(string title) : this()
        {
            Title = title;
        }
    }

    public class EditCategoryViewModel : CategoryFormViewModel
    {
        public Guid Id { get; set; }

        public EditCategoryViewModel() { }

        public EditCategoryViewModel(Guid id, string title) : this()
        {
            Id = id;
            Title = title;
        }
    }

    public class DeleteCategoryViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public DeleteCategoryViewModel(Guid id, string title)
        {
            Id = id;
            Title = title;
        }
    }

    public class ViewCategoriesViewModel
    {
        public List<CategoryDetailsViewModel> Records { get; set; }

        public ViewCategoriesViewModel(List<Category> categories)
        {
            Records = new List<CategoryDetailsViewModel>();

            foreach (var c in categories)
                Records.Add(new CategoryDetailsViewModel(c.Id, c.Title, c.Expenses));
        }
    }

    public class CategoryDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<ExpenseDetailsViewModel> Expenses { get; set; }
        public decimal TotalExpenses { get; set; }

        public CategoryDetailsViewModel(Guid id, string title, List<Expense> expenses)
        {
            Id = id;
            Title = title;
            Expenses = new List<ExpenseDetailsViewModel>();

            foreach (var expense in expenses)
            {
                TotalExpenses += expense.Value;

                var expenseDetails = new ExpenseDetailsViewModel(
                    expense.Id,
                    expense.Description,
                    expense.Value,
                    expense.OccurrenceDate,
                    expense.PaymentMethod,
                    expense.Categories
                );

                Expenses.Add(expenseDetails);
            }
        }
    }
}
