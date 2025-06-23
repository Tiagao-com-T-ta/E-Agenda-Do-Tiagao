using E_Agenda.Domain.CategoriesModule;
using E_Agenda.Structure.CategoriesModule;
using E_Agenda.Structure.Shared;
using E_Agenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Agenda.WebApp.Controllers

  
{
    [Route("categories")]
    public class CategoriesController : Controller
    {
        private readonly DataContext dataContext;
        private readonly ICategoriesRepository categoriesRepository;

        public CategoriesController()
        {
            dataContext = new DataContext(true);
            categoriesRepository = new CategoriesRepositoryFile(dataContext);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var registries = categoriesRepository.GetAllRegisters();
            return View();

        }

        [HttpGet("register")]

        public IActionResult Register()
        {
            var registerVM = new RegisterCategoriesViewModel();
            return View();
        }
    }
}
