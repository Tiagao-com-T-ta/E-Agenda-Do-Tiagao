
using TaskManagement.Domain.TaskModule;
using TaskManagement.WebApp.ViewModels;

namespace TaskManagement.WebApp.Extensions
{
    public static class TaskExtensions
    {
        public static TasksClass ToEntity(this TaskFormViewModel viewModel)
        {
            return new TasksClass(viewModel.Title, viewModel.Priority);
        }

        public static TaskDetailsViewModel ToDetailsViewModel(this TasksClass task)
        {
            return new TaskDetailsViewModel(
                task.Id,
                task.Title,
                task.Priority,
                task.CreationDate,
                task.CompletionDate,
                task.IsCompleted,
                task.CompletionPercentage,
                task.Items
            );
        }

        public static TaskFormViewModel ToFormViewModel(this TasksClass task)
        {
            return new TaskFormViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Priority = task.Priority
            };
        }

        public static TaskItemViewModel ToViewModel(this TaskItem item)
        {
            return new TaskItemViewModel(
                item.Id,
                item.Title,
                item.IsCompleted
            );
        }
    }
}