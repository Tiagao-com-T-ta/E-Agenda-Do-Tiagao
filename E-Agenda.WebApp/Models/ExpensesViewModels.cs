using E_Agenda.Domain.CategoriesModule;

namespace E_Agenda.WebApp.Models
{
    public abstract class ExpensesViewModels
    {
        public string Description { get; set; }

        public DateTime When { get; set; }

        public string Amount { get; set; }

        public string PaymentMethod { get; set; }

        public List<Category> Category { get; set; }

    }

    public class RegisterExpensesViewModels : ExpensesViewModels
    {
        public RegisterExpensesViewModels() { }

        public RegisterExpensesViewModels(string description, DateTime when, string amount, string paymentMethod, Category category) : this()
        {
            Description = description;
            When = when;
            Amount = amount;
            PaymentMethod = paymentMethod;
            
        }
    }

    public class EditExpensesViewModels : ExpensesViewModels
    {
        public string Description { get; set; }

        public DateTime When { get; set; }

        public string Amount { get; set; }

        public string PaymentMethod { get; set; }

        public List<Category> Category { get; set; }
        public EditExpensesViewModels() { }

        public EditExpensesViewModels(string description, DateTime when, string amount, string paymentMethod, Category category)
        {
            Description = description;
            When = when;
            Amount = amount;
            PaymentMethod = paymentMethod;
        }
    }

    public class DeleteExpensesViewModels
    {
        public string Description { get; set; }

        public DateTime When { get; set; }

        public string Amount { get; set; }

        public string PaymentMethod { get; set; }

        public List<Category> Category { get; set; }

        public DeleteExpensesViewModels() { }

        public DeleteExpensesViewModels(string description, DateTime when, string amount, string paymentMethod, Category category) : this()
        {
            Description = description;
            When = when;
            Amount = amount;
            PaymentMethod = paymentMethod;
        }

    }

    public class ViewExpensesViewModels
    {
        public List<CategoriesDetailsViewModel> Registry { get; }

        public ViewExpensesViewModels(List<Category> categories)
        {
            Registry = [];

            foreach (var category in categories)
            {
                //var detailsVM = category.ForDetailsVM();

               //Registry.Add(detailsVM);
            }
        }
    }

    public class ExpensesDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public DateTime When { get; set; }

        public string Amount { get; set; }

        public string PaymentMethod { get; set; }

        public List<Category> Categories { get; set; }

        public ExpensesDetailsViewModel(string description, DateTime when, string amount, string paymentMethod, List<Category> categories)
        {
            Description = description;
            When = when;
            Amount = amount;
            PaymentMethod = paymentMethod;
            Categories = categories;
        }
    }

    public class SelectExpensesViewModels
    {
        public string Description { get; set; }

        public DateTime When { get; set; }

        public string Amount { get; set; }

        public string PaymentMethod { get; set; }

        public List<Category> Category { get; set; }

        public SelectExpensesViewModels(string description, DateTime when, string amount, string paymentMethod, Category category)
        {
            Description = description;
            When = when;
            Amount = amount;
            PaymentMethod = paymentMethod;

        }
    }
}
