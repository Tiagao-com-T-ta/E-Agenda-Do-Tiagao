using E_Agenda.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagement.Domain.TaskModule;

namespace TaskManagement.Domain.TaskModule
{
    public class TasksClass : BaseEntity<TasksClass>
    {
        public string Title { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool IsCompleted { get; set; }
        public decimal CompletionPercentage { get; set; }
        public List<TaskItem> Items { get; set; }

        public TasksClass()
        {
            Id = Guid.NewGuid();
            Items = new List<TaskItem>();
            CreationDate = DateTime.Now;
            IsCompleted = false;
            CompletionPercentage = 0;
        }

        public TasksClass(string title, TaskPriority priority) : this()
        {
            Title = title;
            Priority = priority;
        }

        public void AddItem(TaskItem item)
        {
            Items.Add(item);
            CalculateCompletionPercentage();
        }

        public void RemoveItem(TaskItem item)
        {
            Items.Remove(item);
            CalculateCompletionPercentage();
        }

        public void CompleteItem(Guid itemId)
        {
            var item = Items.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                item.Complete();
                CalculateCompletionPercentage();
            }
        }

        private void CalculateCompletionPercentage()
        {
            if (Items.Count == 0)
            {
                CompletionPercentage = 0;
                return;
            }

            int completedItems = Items.Count(i => i.IsCompleted);
            CompletionPercentage = (decimal)completedItems / Items.Count * 100;

            if (CompletionPercentage == 100)
            {
                IsCompleted = true;
                CompletionDate = DateTime.Now;
            }
            else
            {
                IsCompleted = false;
                CompletionDate = null;
            }
        }

        public override void Update(TasksClass editedRegister)
        {
            Title = editedRegister.Title;
            Priority = editedRegister.Priority;
            IsCompleted = editedRegister.IsCompleted;
            CompletionPercentage = editedRegister.CompletionPercentage;
        }
    }
}
