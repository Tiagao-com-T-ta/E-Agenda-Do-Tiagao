using E_Agenda.Domain.CategoriesModule;
using E_Agenda.Domain.ExpensesModule;
using E_Agenda.WebApp.Models;

namespace E_Agenda.WebApp.Extensions
{
    public static class CategoriesExtensions
    {
        public static Category ForEntity(this CategoriesViewModels categoriesVM)
        {
            return new Category(categoriesVM.Title);
        }

        public static CategoriesDetailsViewModel ForDetailsVM(this Guid id, Category categories, List<Expense> expenses)
        {
            return new CategoriesDetailsViewModel(categories.Id, categories.Title, categories.Expenses);
        }
    }
}
