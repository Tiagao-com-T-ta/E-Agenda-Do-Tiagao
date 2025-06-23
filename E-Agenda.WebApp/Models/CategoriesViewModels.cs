using E_Agenda.Domain.CategoriesModule;
using E_Agenda.Domain.ExpensesModule;

namespace E_Agenda.WebApp.Models
{
    public abstract class CategoriesViewModels
    {
        public string Title { get; set; }
        public List<Expense> Expenses { get; }

    }

    public class RegisterCategoriesViewModel : CategoriesViewModels
    {
        public RegisterCategoriesViewModel() { }

        public RegisterCategoriesViewModel(string title) : this()
        {
            Title = title;
        }
    }

    public class EditCategoriesViewModel : CategoriesViewModels
    {
        public Guid Id { get; set; }
        public EditCategoriesViewModel() { }

        public EditCategoriesViewModel(Guid id, string title)
        {
            Id = id;
            Title = title;
        }
    }

    public class DeleteCategoriesViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public DeleteCategoriesViewModel() { }

        public DeleteCategoriesViewModel(Guid id, string title) : this()
        {
            Id = id;
            Title = title;
        }

    }

    public class ViewCategoriesViewModel
    {
        public List<CategoriesDetailsViewModel> Registry { get; }

        public ViewCategoriesViewModel(List<Category> categories)
        {
            Registry = [];

            foreach (var category in categories)
            {
               // var detailsVM = category.ForDetailsVM();

              //  Registry.Add(detailsVM);
            }
        }
    }

    public class CategoriesDetailsViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public CategoriesDetailsViewModel(Guid id, string title, List<Expense> expenses)
        { 
            Id = id;
            Title = title;        
        }
    }

    public class SelectCategoriesViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public SelectCategoriesViewModel(Guid id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
