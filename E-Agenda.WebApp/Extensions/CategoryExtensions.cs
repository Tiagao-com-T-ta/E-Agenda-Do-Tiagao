using E_Agenda.Domain.CategoriesModule;
using E_Agenda.Domain.CategoryModule;
using E_Agenda.WebApp.Models;

namespace E_Agenda.WebApp.Extensions;

public static class CategoryExtensions
{
    public static Category ToEntity(this CategoryFormViewModel formVM)
    {
        return new Category(formVM.Title);
    }

    public static CategoryDetailsViewModel ToDetailsVM(this Category category)
    {
        return new CategoryDetailsViewModel(
            category.Id,
            category.Title,
            category.Expenses
        );
    }
}
