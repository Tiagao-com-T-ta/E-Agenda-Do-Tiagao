using E_Agenda.Domain.AppointmentModule;
using E_Agenda.Domain.ContactModule;
using E_Agenda.Structure.AppointmentModule;
using E_Agenda.Structure.ContactModule;
using E_Agenda.Structure.Shared;
using E_Agenda.WebApp.Extensions;
using E_Agenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Agenda.WebApp.Controllers
{
    [Route("appointments")]
    public class AppointmentController : Controller
    {
        private readonly DataContext dataContext;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IContactRepository contactRepository;

        public AppointmentController()
        {
            dataContext = new DataContext(true);
            appointmentRepository = new AppointmentRepositoryFile(dataContext);
            contactRepository = new ContactRepositoryFile(dataContext);

        }
        public IActionResult Index()
        {
            var appointments = appointmentRepository.GetAllRegisters();

            var ViewVM = new ViewAppointmentsViewModel(appointments);

            return View(ViewVM);
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            var registerVM = new RegisterAppointmentViewModel();
            registerVM.Contacts = contactRepository.GetAllRegisters()
            .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
            .ToList();

            return View(registerVM);
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterAppointmentViewModel registerVM)
        {
            var registers = appointmentRepository.GetAllRegisters(); 

            foreach (var register in registers)
            {
                if (register.Date == registerVM.Date && register.StartTime == registerVM.StartTime)
                {
                    ModelState.AddModelError("Data Conflitante", "Já existe um compromisso marcado para este período.");
                    break;
                }

                if (registerVM.Type == "Presencial" && string.IsNullOrWhiteSpace(registerVM.Location))
                {
                    ModelState.AddModelError(nameof(registerVM.Location), "O campo \"Local\" é obrigatório para compromissos presenciais.");
                    break;
                }

                if (registerVM.Type == "Online" && string.IsNullOrWhiteSpace(registerVM.Link))
                {
                    ModelState.AddModelError(nameof(registerVM.Link), "O campo \"Link\" é obrigatório para compromissos Online.");
                    break;
                }
                if (!ModelState.IsValid)
                {
                    registerVM.Contacts = contactRepository.GetAllRegisters()
                        .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                        .ToList();
                    return View(registerVM);
                }

            }

            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var entity = registerVM.ToEntity();

            appointmentRepository.Register(entity);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id:guid}")]
        public ActionResult Edit(Guid id)
        {
            var selectedRegister = appointmentRepository.GetRegisterById(id);

            var editVM = new EditAppointmentViewModel(
                selectedRegister.Id,
                selectedRegister.Topic,
                selectedRegister.Date,
                selectedRegister.StartTime,
                selectedRegister.EndTime,
                selectedRegister.Type,
                selectedRegister.Location,  
                selectedRegister.Link       
            );

            return View(editVM);
        }

        [HttpPost("edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, EditAppointmentViewModel model)
        {
            var registers = appointmentRepository.GetAllRegisters();

            foreach (var reg in registers)
            {
                if (!reg.Id.Equals(id) && reg.Date.Equals(model.Date) && reg.StartTime.Equals(model.StartTime))
                {
                    ModelState.AddModelError(nameof(model.Date), "Já existe um compromisso marcado neste horário.");
                    break;

                }

                if (!reg.Id.Equals(id) && model.Type.Equals("Presencial") && string.IsNullOrWhiteSpace(model.Location))
                {
                    ModelState.AddModelError(nameof(model.Location), "O campo \"Local\" é obrigatório para compromissos presenciais.");
                    break;
                }
                if (!reg.Id.Equals(id) && model.Type.Equals("Online") && string.IsNullOrWhiteSpace(model.Link))
                {
                    ModelState.AddModelError(nameof(model.Link), "O campo \"Link\" é obrigatório para compromissos Online.");
                    break;
                }


            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var editedEntity = model.ToEntity();

            appointmentRepository.Edit(id, editedEntity);

            return RedirectToAction(nameof(Index));


        }

        [HttpGet("delete/{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            var selectedRegister = appointmentRepository.GetRegisterById(id);

            var deleteVM = new DeleteAppointmentViewModel(
                id,
                selectedRegister.Topic
                );

            return View(deleteVM);

        }

        [HttpPost("delete/{id:guid}")]
        public IActionResult ConfirmDelete(Guid id)
        {
            appointmentRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("details/{id:guid}")]
        public IActionResult Details(Guid id)
        {
            var selectedRegister = appointmentRepository.GetRegisterById(id);
            var detailsVM = new AppointmentDetailsViewModel(
                selectedRegister.Id,
                selectedRegister.Topic,
                selectedRegister.Date,
                selectedRegister.StartTime,
                selectedRegister.EndTime,
                selectedRegister.Type,
                selectedRegister.Link,      
                selectedRegister.Location   
            );

            return View(detailsVM);
        }
    }
}
