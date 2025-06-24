using E_Agenda.Domain.CategoriesModule;
using E_Agenda.Domain.ExpenseModule;
using E_Agenda.Structure.Shared;
using E_Agenda.Structure.CategoriesModule;
using E_Agenda.Structure.ExpensesModule;
using E_Agenda.WebApp.Extensions;
using E_Agenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using E_Agenda.Domain.ExpensesModule;

namespace E_Agenda.WebApp.Controllers
{
    [Route("expenses")]
    public class ExpenseController : Controller
    {
        private readonly DataContext context;
        private readonly IExpensesRepository expensesRepository;
        private readonly ICategoryRepository categoriesRepository;

        public ExpenseController()
        {
            context = new DataContext(true);
            expensesRepository = new ExpensesRepositoryFile(context);
            categoriesRepository = new CategoryRepositoryFile(context);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var records = expensesRepository.GetAllRegisters();
            var viewModel = new VisualizeExpensesViewModel(records);

            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            var availableCategories = categoriesRepository.GetAllRegisters();

            var viewModel = new RegisterExpenseViewModel(availableCategories);

            return View(viewModel);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RegisterExpenseViewModel viewModel)
        {
            var availableCategories = categoriesRepository.GetAllRegisters();

            if (!ModelState.IsValid)
            {
                foreach (var category in availableCategories)
                {
                    var selectItem = new SelectListItem(category.Title, category.Id.ToString());
                    viewModel.AvailableCategories?.Add(selectItem);
                }

                return View(viewModel);
            }

            var expense = viewModel.ToEntity();

            if (viewModel.SelectedCategories is not null)
            {
                foreach (var selectedId in viewModel.SelectedCategories)
                {
                    var category = availableCategories.FirstOrDefault(c => c.Id == selectedId);
                    if (category is not null)
                        expense.RegisterCategory(category);
                }
            }

            expensesRepository.Register(expense);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id:guid}")]
        public IActionResult Edit(Guid id)
        {
            var availableCategories = categoriesRepository.GetAllRegisters();

            var selectedExpense = expensesRepository.GetRegisterById(id);

            var viewModel = new EditExpenseViewModel(
                id,
                selectedExpense.Description,
                selectedExpense.Value,
                selectedExpense.OccurrenceDate,
                selectedExpense.PaymentMethod,
                selectedExpense.Categories,
                availableCategories
            );

            return View(viewModel);
        }

        [HttpPost("edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, EditExpenseViewModel viewModel)
        {
            var availableCategories = categoriesRepository.GetAllRegisters();

            if (!ModelState.IsValid)
            {
                foreach (var category in availableCategories)
                {
                    var selectItem = new SelectListItem(category.Title, category.Id.ToString());
                    viewModel.AvailableCategories?.Add(selectItem);
                }

                return View(viewModel);
            }

            var editedExpense = viewModel.ToEntity();

            var selectedExpense = expensesRepository.GetRegisterById(id);

            foreach (var category in selectedExpense.Categories.ToList())
                selectedExpense.RemoveCategory(category);

            if (viewModel.SelectedCategories is not null)
            {
                foreach (var selectedId in viewModel.SelectedCategories)
                {
                    var category = availableCategories.FirstOrDefault(c => c.Id == selectedId);
                    if (category is not null)
                        selectedExpense.RegisterCategory(category);
                }
            }

            expensesRepository.Edit(id, editedExpense);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("delete/{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var selectedExpense = expensesRepository.GetRegisterById(id);

            var viewModel = new DeleteExpenseViewModel(selectedExpense.Id, selectedExpense.Description);

            return View(viewModel);
        }

        [HttpPost("delete/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            expensesRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("details/{id:guid}")]
        public IActionResult Details(Guid id)
        {
            var selectedExpense = expensesRepository.GetRegisterById(id);

            var viewModel = new ExpenseDetailsViewModel(
                id,
                selectedExpense.Description,
                selectedExpense.Value,
                selectedExpense.OccurrenceDate,
                selectedExpense.PaymentMethod,
                selectedExpense.Categories
            );

            return View(viewModel);
        }
    }
}
