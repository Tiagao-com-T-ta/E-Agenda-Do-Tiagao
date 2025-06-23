using E_Agenda.Domain.CategoryModule;
using E_Agenda.WebApp.Models;

namespace E_Agenda.WebApp.Extensions
{
    public static class CategoryExtensions
    {
        public static Category ToEntity(this CategoryViewModel categoryVM)
        {
            return new Category(categoryVM.Title);
        }

        public static CategoryDetailsViewModel ToDetailsViewModel(this Category category)
        {
            return new CategoryDetailsViewModel(category);
        }

        public static EditCategoryViewModel ToEditViewModel(this Category category)
        {
            return new EditCategoryViewModel
            {
                Id = category.Id,
                Title = category.Title
            };
        }
    }
}