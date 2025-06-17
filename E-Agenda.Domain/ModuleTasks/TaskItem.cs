namespace TaskManagement.Domain.TaskModule
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }

        public TaskItem()
        {
            Id = Guid.NewGuid();
            IsCompleted = false;
        }

        public TaskItem(string title) : this()
        {
            Title = title;
        }

        public void Complete()
        {
            IsCompleted = true;
        }
    }
}