using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.TaskModule;
using TaskManagement.Infrastructure.TaskModule;
using TaskManagement.WebApp.ViewModels;
using TaskManagement.WebApp.Extensions;
using E_Agenda.Structure.Shared;

namespace TaskManagement.WebApp.Controllers;

[Route("tasks")]
public class TasksController : Controller
{
    private readonly DataContext context;
    private readonly ITasksRepository tasksRepository;

    public TasksController()
    {
        context = new DataContext(true);
        tasksRepository = new TasksRepositoryInFile(context);
    }

    [HttpGet]
    public IActionResult Index()
    {
        var tarefas = tasksRepository.GetAllRegisters();
        var viewModel = new TaskListViewModel(tarefas);

        return View(viewModel);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var cadastrarVM = new TaskFormViewModel();
        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(TaskFormViewModel cadastrarVM)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVM);

        var novaTarefa = cadastrarVM.ToEntity();

        tasksRepository.Register(novaTarefa);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("editar/{id:guid}")]
    public IActionResult Editar(Guid id)
    {
        var tarefa = tasksRepository.GetRegisterById(id);

        if (tarefa == null)
            return NotFound();

        var editarVM = tarefa.ToFormViewModel();

        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Guid id, TaskFormViewModel editarVM)
    {
        if (!ModelState.IsValid)
            return View(editarVM);

        var tarefaExistente = tasksRepository.GetRegisterById(id);
        if (tarefaExistente == null)
            return NotFound();

        var tarefaAtualizada = editarVM.ToEntity();
        tarefaExistente.Update(tarefaAtualizada);

        tasksRepository.Edit(id, tarefaExistente);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        var tarefa = tasksRepository.GetRegisterById(id);

        if (tarefa == null)
            return NotFound();

        var excluirVM = tarefa.ToDetailsViewModel();

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        var tarefa = tasksRepository.GetRegisterById(id);

        if (tarefa == null)
            return NotFound();

        bool possuiItensPendentes = tarefa.Items.Any(i => !i.IsCompleted);

        if (possuiItensPendentes)
        {
            var viewModel = tarefa.ToDetailsViewModel();
            
            ViewBag.Erro = "A tarefa não pode ser excluída pois possui itens pendentes.";

            return View("Excluir", viewModel);
        }

        tasksRepository.Delete(id);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("detalhes/{id:guid}")]
    public IActionResult Detalhes(Guid id)
    {
        var tarefa = tasksRepository.GetRegisterById(id);

        if (tarefa == null)
            return NotFound();

        var detalhesVM = tarefa.ToDetailsViewModel();

        return View(detalhesVM);
    }
    [HttpGet("{id:guid}/items/adicionar")]
    public IActionResult AdicionarItem(Guid id)
    {
        var tarefa = tasksRepository.GetRegisterById(id);
        if (tarefa == null)
            return NotFound();

        var viewModel = new ManageTaskItemsViewModel(tarefa);
        return View(viewModel);
    }

    [HttpPost("{id:guid}/items/adicionar")]
    [ValidateAntiForgeryToken]
    public IActionResult AdicionarItem(Guid id, AddTaskItemViewModel newItem)
    {
        var tarefa = tasksRepository.GetRegisterById(id);
        if (tarefa == null)
            return NotFound();

        if (!ModelState.IsValid)
        {
            var viewModelInvalid = new ManageTaskItemsViewModel(tarefa)
            {
                NewItem = newItem
            };
            return View(viewModelInvalid);
        }

        var taskItem = new TaskItem(newItem.Title);
        tarefa.AddItem(taskItem);

        tasksRepository.Edit(id, tarefa);

        return RedirectToAction("Detalhes", new { id });
    }

    [HttpPost("{id:guid}/items/concluir")]
    public IActionResult ConcluirItem(Guid id, Guid itemId)
    {
        var tarefa = tasksRepository.GetRegisterById(id);
        if (tarefa == null)
            return NotFound();

        var item = tarefa.Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
            return NotFound();

        if (item.IsCompleted)
            item.IsCompleted = false;
        else
            item.Complete();

        tarefa.GetType()
            .GetMethod("CalculateCompletionPercentage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(tarefa, null);

        tasksRepository.Edit(id, tarefa);

        return RedirectToAction("Detalhes", new { id });
    }

}
