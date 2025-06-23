using E_Agenda.Domain.CategoriesModule;
using E_Agenda.Domain.CategoryModule;
using E_Agenda.Structure.Shared;
using E_Agenda.WebApp.Extensions;
using E_Agenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Agenda.WebApp.Controllers
{
    [Route("categories")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly DataContext dataContext;

        public CategoryController(
            ICategoryRepository categoryRepository,
            DataContext dataContext)
        {
            this.categoryRepository = categoryRepository;
            this.dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = categoryRepository.GetAllRegisters();
            var viewModel = new CategoryListViewModel(categories);
            return View(viewModel);
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View(new RegisterCategoryViewModel());
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterCategoryViewModel registerVM)
        {
            if (!ModelState.IsValid)
                return View(registerVM);

            var newCategory = registerVM.ToEntity();
            categoryRepository.Register(newCategory);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id:guid}")]
        public IActionResult Edit(Guid id)
        {
            var category = categoryRepository.GetRegisterById(id);

            if (category == null)
                return NotFound();

            return View(category.ToEditViewModel());
        }

        [HttpPost("edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, EditCategoryViewModel editVM)
        {
            if (!ModelState.IsValid)
                return View(editVM);

            var existingCategory = categoryRepository.GetRegisterById(id);
            if (existingCategory == null)
                return NotFound();

            var updatedCategory = editVM.ToEntity();
            existingCategory.Update(updatedCategory);

            categoryRepository.Edit(id, existingCategory);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("details/{id:guid}")]
        public IActionResult Details(Guid id)
        {
            var category = categoryRepository.GetRegisterById(id);

            if (category == null)
                return NotFound();

            return View(category.ToDetailsViewModel());
        }

        [HttpGet("delete/{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var category = categoryRepository.GetRegisterById(id);

            if (category == null)
                return NotFound();

            return View(category.ToDetailsViewModel());
        }

        [HttpPost("delete/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var category = categoryRepository.GetRegisterById(id);

            if (category == null)
                return NotFound();

            if (category.Expenses.Count > 0)
            {
                var viewModel = category.ToDetailsViewModel();
                ViewBag.Error = "Category cannot be deleted because it has associated expenses.";
                return View("Delete", viewModel);
            }

            categoryRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}