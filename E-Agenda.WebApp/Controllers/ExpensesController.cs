using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using E_Agenda.Domain.ExpenseModule;
using E_Agenda.Domain.CategoryModule;
using E_Agenda.WebApp.Models;
using E_Agenda.WebApp.Extensions;
using System;
using System.Linq;
using E_Agenda.Domain.CategoriesModule;
using E_Agenda.Domain.ExpensesModule;
using E_Agenda.Structure.CategoriesModule;
using E_Agenda.Structure.ExpensesModule;
using E_Agenda.Structure.Shared;

namespace E_Agenda.WebApp.Controllers
{
    [Route("expenses")]
    public class ExpensesController : Controller
    {
        private readonly IExpensesRepository expensesRepository;
        private readonly ICategoryRepository categoriesRepository;

        public ExpensesController()
        {
            var context = new DataContext(true);
            expensesRepository = new ExpensesRepositoryFile(context);
            categoriesRepository = new CategoryRepositoryFile(context);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var expenses = expensesRepository.GetAllRegisters();
            var viewModel = new ExpenseListViewModel(expenses);
            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            var viewModel = new RegisterExpenseViewModel
            {
                SelectedCategories = new System.Collections.Generic.List<Category>()
            };

            ViewBag.Categories = categoriesRepository.GetAllRegisters()
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Title })
                .ToList();

            return View("Create", viewModel);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RegisterExpenseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = categoriesRepository.GetAllRegisters()
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Title })
                    .ToList();
                return View(viewModel);
            }
            var selectedCategoryIds = viewModel.SelectedCategories.Select(c => c.Id).ToList();
            var categories = categoriesRepository.GetAllRegisters()
                .Where(c => selectedCategoryIds.Contains(c.Id))
                .ToList();

            var newExpense = viewModel.ToEntity();
            newExpense.Category = categories;

            expensesRepository.Register(newExpense);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id:guid}")]
        public IActionResult Edit(Guid id)
        {
            var expense = expensesRepository.GetRegisterById(id);
            if (expense == null)
                return NotFound();

            var viewModel = expense.ToEditViewModel();

            ViewBag.Categories = categoriesRepository.GetAllRegisters()
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Title })
                .ToList();

            return View(viewModel);
        }

        [HttpPost("edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, EditExpenseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = categoriesRepository.GetAllRegisters()
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Title })
                    .ToList();
                return View(viewModel);
            }

            var existingExpense = expensesRepository.GetRegisterById(id);
            if (existingExpense == null)
                return NotFound();

            var selectedCategoryIds = viewModel.SelectedCategories.Select(c => c.Id).ToList();
            var categories = categoriesRepository.GetAllRegisters()
                .Where(c => selectedCategoryIds.Contains(c.Id))
                .ToList();

            var updatedExpense = viewModel.ToEntity();
            updatedExpense.Category = categories;

            existingExpense.Update(updatedExpense);
            expensesRepository.Edit(id, existingExpense);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("details/{id:guid}")]
        public IActionResult Details(Guid id)
        {
            var expense = expensesRepository.GetRegisterById(id);
            if (expense == null)
                return NotFound();

            var viewModel = expense.ToDetailsViewModel();
            return View(viewModel);
        }

        [HttpGet("delete/{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var expense = expensesRepository.GetRegisterById(id);
            if (expense == null)
                return NotFound();

            var viewModel = expense.ToDetailsViewModel();
            return View(viewModel);
        }

        [HttpPost("delete/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var expense = expensesRepository.GetRegisterById(id);
            if (expense == null)
                return NotFound();

            expensesRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
