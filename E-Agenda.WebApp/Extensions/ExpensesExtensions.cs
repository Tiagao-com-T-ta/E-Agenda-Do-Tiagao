using E_Agenda.Domain.ExpenseModule;
using E_Agenda.WebApp.Models;

namespace E_Agenda.WebApp.Extensions
{
    public static class ExpenseExtensions
    {
        public static Expense ToEntity(this ExpenseViewModel expenseVM)
        {
            return new Expense(
                expenseVM.Description,
                expenseVM.Date,
                expenseVM.Amount,
                expenseVM.PaymentMethod,
                expenseVM.SelectedCategories);
        }

        public static ExpenseDetailsViewModel ToDetailsViewModel(this Expense expense)
        {
            return new ExpenseDetailsViewModel(expense);
        }

        public static EditExpenseViewModel ToEditViewModel(this Expense expense)
        {
            return new EditExpenseViewModel
            {
                Id = expense.Id,
                Description = expense.Description,
                Date = expense.Date,
                Amount = expense.Amount,
                PaymentMethod = expense.PaymentMethod,
                SelectedCategories = expense.Category
            };
        }
    }
}