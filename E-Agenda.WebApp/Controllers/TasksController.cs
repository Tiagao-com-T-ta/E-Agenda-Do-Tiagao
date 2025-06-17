using E_Agenda.Structure.Shared;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.TaskModule;
using TaskManagement.Infrastructure.TaskModule;
using TaskManagement.WebApp.Extensions;
using TaskManagement.WebApp.ViewModels;

namespace TaskManagement.WebApp.Controllers
{
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly DataContext context;
        private readonly ITasksRepository tasksRepository;

        public TaskController()
        {
            context = new DataContext(loadData: true);
            tasksRepository = new TasksRepositoryInFile(context);
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string status)
        {
            List<TasksClass> tasks = status switch
            {
                "pending" => tasksRepository.GetPendingTasks(),
                "completed" => tasksRepository.GetCompletedTasks(),
                _ => tasksRepository.GetAllRegisters()
            };

            return Ok(new TaskListViewModel(tasks));
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var task = tasksRepository.GetRegisterById(id);
            if (task == null)
                return NotFound();

            return Ok(task.ToDetailsViewModel());
        }

        [HttpPost]
        public IActionResult Create([FromBody] TaskFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = viewModel.ToEntity();
            tasksRepository.Register(task);

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task.ToDetailsViewModel());
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] TaskFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingTask = tasksRepository.GetRegisterById(id);
            if (existingTask == null)
                return NotFound();

            var updatedTask = viewModel.ToEntity();
            existingTask.Update(updatedTask);

            tasksRepository.Edit(id, existingTask);

            return Ok(existingTask.ToDetailsViewModel());
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var task = tasksRepository.GetRegisterById(id);
            if (task == null)
                return NotFound();

            tasksRepository.Delete(id);
            return NoContent();
        }

        [HttpGet("{id:guid}/items")]
        public IActionResult GetTaskItems(Guid id)
        {
            var task = tasksRepository.GetRegisterById(id);
            if (task == null)
                return NotFound();

            return Ok(task.Items.ConvertAll(i => i.ToViewModel()));
        }

        [HttpPost("{id:guid}/items")]
        public IActionResult AddItem(Guid id, [FromBody] AddTaskItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = tasksRepository.GetRegisterById(id);
            if (task == null)
                return NotFound();

            var item = new TaskItem(viewModel.Title);
            task.AddItem(item);

            tasksRepository.Edit(id, task);

            return Ok(item.ToViewModel());
        }

        [HttpDelete("{id:guid}/items/{itemId:guid}")]
        public IActionResult RemoveItem(Guid id, Guid itemId)
        {
            var task = tasksRepository.GetRegisterById(id);
            if (task == null)
                return NotFound();

            var item = task.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                return NotFound();

            task.RemoveItem(item);
            tasksRepository.Edit(id, task);

            return NoContent();
        }

        [HttpPost("{id:guid}/items/{itemId:guid}/complete")]
        public IActionResult CompleteItem(Guid id, Guid itemId)
        {
            var task = tasksRepository.GetRegisterById(id);
            if (task == null)
                return NotFound();

            task.CompleteItem(itemId);
            tasksRepository.Edit(id, task);

            return Ok(task.ToDetailsViewModel());
        }

        [HttpGet("priority/{priority}")]
        public IActionResult GetByPriority(TaskPriority priority)
        {
            var tasks = tasksRepository.GetTasksByPriority(priority);
            return Ok(new TaskListViewModel(tasks));
        }
    }
}