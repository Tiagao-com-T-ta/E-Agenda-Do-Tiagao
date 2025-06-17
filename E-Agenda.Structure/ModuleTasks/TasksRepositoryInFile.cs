using TaskManagement.Domain.TaskModule;
using E_Agenda.Structure.Shared;
using TaskManagement.Domain;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagement.Infrastructure.TaskModule
{
    public class TasksRepositoryInFile : BaseRepositoryFile<TasksClass>, ITasksRepository
    {
        public TasksRepositoryInFile(DataContext dataContext) : base(dataContext)
        {
        }

        protected override List<TasksClass> GetRegisters()
        {
            return dataContext.TasksClass;
        }

        public List<TasksClass> GetPendingTasks()
        {
            return registers.Where(t => !t.IsCompleted).ToList();
        }

        public List<TasksClass> GetCompletedTasks()
        {
            return registers.Where(t => t.IsCompleted).ToList();
        }

        public List<TasksClass> GetTasksByPriority(TaskPriority priority)
        {
            return registers.Where(t => t.Priority == priority).ToList();
        }
    }
}
