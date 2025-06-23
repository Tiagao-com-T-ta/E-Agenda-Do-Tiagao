using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaskManagement.Domain.TaskModule;
using TaskManagement.WebApp.Extensions;

namespace TaskManagement.WebApp.ViewModels
{
    public class TaskFormViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Priority is required")]
        public TaskPriority Priority { get; set; }
    }

    public class TaskDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool IsCompleted { get; set; }
        public decimal CompletionPercentage { get; set; }
        public List<TaskItemViewModel> Items { get; set; }

        public TaskDetailsViewModel(
            Guid id,
            string title,
            TaskPriority priority,
            DateTime creationDate,
            DateTime? completionDate,
            bool isCompleted,
            decimal completionPercentage,
            List<TaskItem> items)
        {
            Id = id;
            Title = title;
            Priority = priority;
            CreationDate = creationDate;
            CompletionDate = completionDate;
            IsCompleted = isCompleted;
            CompletionPercentage = completionPercentage;
            Items = items.ConvertAll(i => i.ToViewModel());
        }
    }

    public class TaskItemViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }

        public TaskItemViewModel(Guid id, string title, bool isCompleted)
        {
            Id = id;
            Title = title;
            IsCompleted = isCompleted;
        }
    }

    public class TaskListViewModel
    {
        public List<TaskDetailsViewModel> Tasks { get; set; }

        public TaskListViewModel(List<TasksClass> tasks)
        {
            Tasks = tasks.ConvertAll(t => t.ToDetailsViewModel());
        }
    }

    public class ManageTaskItemsViewModel
    {
        public TaskDetailsViewModel Task { get; set; }
        public AddTaskItemViewModel NewItem { get; set; }

        public ManageTaskItemsViewModel(TasksClass task)
        {
            Task = task.ToDetailsViewModel();
            NewItem = new AddTaskItemViewModel();
        }
    }

    public class AddTaskItemViewModel
    {
        [Required(ErrorMessage = "Item title is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Item title must be between 2 and 100 characters")]
        public string Title { get; set; }
    }
}