using E_Agenda.Domain.CategoriesModule;
using E_Agenda.Structure.CategoriesModule;
using E_Agenda.Structure.Shared;
using E_Agenda.WebApp.Extensions;
using E_Agenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Agenda.WebApp.Controllers
{
    [Route("categories")]
    public class CategoryController : Controller
    {
        private readonly DataContext dataContext;
        private readonly ICategoryRepository categoryRepository;

        public CategoryController()
        {
            dataContext = new DataContext(true);
            categoryRepository = new CategoryRepositoryFile(dataContext);  
        }

        [HttpGet]
        public IActionResult Index()
        {
            var records = categoryRepository.GetAllRegisters();

            var viewModel = new ViewCategoriesViewModel(records);

            return View(viewModel);
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            var registerVM = new RegisterCategoryViewModel();

            return View(registerVM);
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterCategoryViewModel registerVM)
        {
            var records = categoryRepository.GetAllRegisters();

            foreach (var item in records)
            {
                if (item.Title.Equals(registerVM.Title, StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("UniqueRegistration", "A category with this title is already registered.");
                    break;
                }
            }

            if (!ModelState.IsValid)
                return View(registerVM);

            var entity = registerVM.ToEntity();

            categoryRepository.Register(entity);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id:guid}")]
        public IActionResult Edit(Guid id)
        {
            var selectedRecord = categoryRepository.GetRegisterById(id);

            if (selectedRecord == null)
                return NotFound();

            var editVM = new EditCategoryViewModel(id, selectedRecord.Title);

            return View(editVM);
        }

        [HttpPost("edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, EditCategoryViewModel editVM)
        {
            var records = categoryRepository.GetAllRegisters();

            foreach (var item in records)
            {
                if (!item.Id.Equals(id) && item.Title.Equals(editVM.Title, StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("UniqueRegistration", "A category with this title is already registered.");
                    break;
                }
            }

            if (!ModelState.IsValid)
                return View(editVM);

            var editedEntity = editVM.ToEntity();

            categoryRepository.Edit(id, editedEntity);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("delete/{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var selectedRecord = categoryRepository.GetRegisterById(id);

            if (selectedRecord == null)
                return NotFound();

            var deleteVM = new DeleteCategoryViewModel(selectedRecord.Id, selectedRecord.Title);

            return View(deleteVM);
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
                var viewModel = category.ToDetailsVM();
                ViewBag.Error = "Category cannot be deleted because it has associated expenses.";
                return View("Delete", viewModel);
            }

            categoryRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("details/{id:guid}")]
        public IActionResult Details(Guid id)
        {
            var selectedRecord = categoryRepository.GetRegisterById(id);

            if (selectedRecord == null)
                return NotFound();

            var detailsVM = new CategoryDetailsViewModel(selectedRecord.Id, selectedRecord.Title, selectedRecord.Expenses);

            return View(detailsVM);
        }
    }
}
