using E_Agenda.Domain.CategoriesModule;
using E_Agenda.Domain.ExpensesModule;
using E_Agenda.Structure.CategoriesModule;
using E_Agenda.Structure.ExpensesModule;
using E_Agenda.Structure.Shared;
using E_Agenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Agenda.WebApp.Controllers

    //[Route("expenses")]
{
    public class ExpensesController : Controller
    {
        private readonly DataContext dataContext;
        private readonly IExpensesRepository expensesRepository;

        public ExpensesController()
        {
            dataContext = new DataContext(true);
            expensesRepository = new ExpensesRepositoryFile(dataContext);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var registries = expensesRepository.GetAllRegisters();
            return View();

        }

        [HttpGet("register")]

        public IActionResult Register()
        {
            var registerVM = new RegisterExpensesViewModels();
            return View();
        }
    }
}
