using E_Agenda.Domain.Shared;
using System;
using System.Collections.Generic;
using TaskManagement.Domain.TaskModule;

namespace TaskManagement.Domain.TaskModule
{
    public interface ITasksRepository : IRepository<TasksClass>
    {
        List<TasksClass> GetPendingTasks();
        List<TasksClass> GetCompletedTasks();
        List<TasksClass> GetTasksByPriority(TaskPriority priority);
    }
}