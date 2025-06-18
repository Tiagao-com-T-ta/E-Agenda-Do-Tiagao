using E_Agenda.Domain.ContactModule;
using E_Agenda.Structure.ContactModule;
using E_Agenda.Structure.Shared;
using E_Agenda.WebApp.Extensions;
using E_Agenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Agenda.WebApp.Controllers
{
    [Route("contacts")]
    public class ContactController : Controller
    {
        private readonly DataContext dataContext;
        private readonly IContactRepository contactRepository;

        public ContactController()
        {
            dataContext = new DataContext(true);
            contactRepository = new ContactRepositoryFile(dataContext);
        }

        public IActionResult Index()
        {
            var contacts = contactRepository.GetAllRegisters();

            var ViewVM = new ViewContactsViewModel(contacts);

            return View(ViewVM);
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            var registerVM = new RegisterContactViewModel();

            return View(registerVM);
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterContactViewModel registerVM)
        {
            var registers = contactRepository.GetAllRegisters();

            foreach (var register in registers)
            {
                if (register.Name == registerVM.Name)
                {
                    ModelState.AddModelError(nameof(registerVM.Name), "Já existe um contato cadastrado com esse nome.");
                    break;
                }

                if (register.Email == registerVM.Email)
                {
                    ModelState.AddModelError(nameof(registerVM.Email), "Já existe um contato cadastrado com esse email.");
                    break;
                }

                if (register.Telephone == registerVM.Telephone)
                {
                    ModelState.AddModelError(nameof(registerVM.Telephone), "Já existe um contato cadastrado com esse telefone.");
                    break;
                }
            }

            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var entity = registerVM.ToEntity();

            contactRepository.Register(entity);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id:guid}")]
        public ActionResult Edit(Guid id)
        {
            var selectedRegister = contactRepository.GetRegisterById(id);

            var editVM = new EditContactViewModel(
                id,
                selectedRegister.Name,
                selectedRegister.Email,
                selectedRegister.Telephone,
                selectedRegister.Role,
                selectedRegister.Company
            );

            return View(editVM);
        }

        [HttpPost("edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, EditContactViewModel model)
        {
            var registers = contactRepository.GetAllRegisters();

            foreach (var reg in registers)
            {
                if (!reg.Id.Equals(id) && reg.Name.Equals(model.Name))
                {
                    ModelState.AddModelError(nameof(model.Name), "Já existe um contato cadastrado com esse nome.");
                    break;

                }

                if (!reg.Id.Equals(id) && reg.Name.Equals(model.Email))
                {
                    ModelState.AddModelError(nameof(model.Email), "Já existe um contato cadastrado com esse Email.");
                    break;

                }

                if (!reg.Id.Equals(id) && reg.Name.Equals(model.Telephone))
                {
                    ModelState.AddModelError(nameof(model.Telephone), "Já existe um contato cadastrado com esse Telefone.");
                    break;

                }

            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var editedEntity = model.ToEntity();

            contactRepository.Edit(id, editedEntity);

            return RedirectToAction(nameof(Index));


        }

        [HttpGet("delete/{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            var selectedRegister = contactRepository.GetRegisterById(id);

            var deleteVM = new DeleteContactViewModel(
                id,
                selectedRegister.Name
                );

            return View(deleteVM);

        }

        [HttpPost("delete/{id:guid}")]
        public IActionResult ConfirmDelete(Guid id)
        {
            contactRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("details/{id:guid}")]
        public IActionResult Details(Guid id)
        {
            var selectedRegister = contactRepository.GetRegisterById(id);

            var detailsVM = new ContactDetailsViewModel(
                id,
                selectedRegister.Name,
                selectedRegister.Email,
                selectedRegister.Telephone,
                selectedRegister.Role,
                selectedRegister.Company
            );

            return View(detailsVM);
        }
    }
}
