using E_Agenda.Domain.CategoryModule;
using E_Agenda.Domain.ExpenseModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Agenda.WebApp.Models
{
    public abstract class CategoryViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 100 characters")]
        public string Title { get; set; }
    }

    public class RegisterCategoryViewModel : CategoryViewModel
    {
        public RegisterCategoryViewModel() { }
    }

    public class EditCategoryViewModel : CategoryViewModel
    {
        public Guid Id { get; set; }
    }

    public class CategoryDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int ExpenseCount { get; set; }
        public decimal TotalExpenses { get; set; }
        public string FormattedTotal => TotalExpenses.ToString("C");

        public CategoryDetailsViewModel(Category category)
        {
            Id = category.Id;
            Title = category.Title;
            ExpenseCount = category.Expenses.Count;
            TotalExpenses = CalculateTotalExpenses(category.Expenses);
        }

        private decimal CalculateTotalExpenses(List<Expense> expenses)
        {
            return expenses.Sum(e => e.Amount);
        }
    }

    public class CategoryListViewModel
    {
        public List<CategoryDetailsViewModel> Categories { get; set; }

        public CategoryListViewModel(List<Category> categories)
        {
            Categories = categories.ConvertAll(c => new CategoryDetailsViewModel(c));
        }
    }
}