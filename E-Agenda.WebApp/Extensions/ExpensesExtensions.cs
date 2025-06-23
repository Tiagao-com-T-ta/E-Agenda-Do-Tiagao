using E_Agenda.Domain.CategoriesModule;
using E_Agenda.Domain.ExpensesModule;
using E_Agenda.WebApp.Models;

namespace E_Agenda.WebApp.Extensions
{
    public static class ExpensesExtensions
    {
        public static Expense ForEntity(this ExpensesViewModels expensesVM)
        {
            return new Expense(expensesVM.Description,
                                expensesVM.When,
                                expensesVM.Amount,
                                expensesVM.PaymentMethod,
                                expensesVM.Category);
        
        }
        public static ExpensesDetailsViewModel ForDetailsVM(this Expense expenses, List<Category> category)
        {
            return new ExpensesDetailsViewModel(expenses.Description, expenses.When, expenses.Amount, expenses.PaymentMethod, 
                                                expenses.Category);
        }
                
        
    }
}
