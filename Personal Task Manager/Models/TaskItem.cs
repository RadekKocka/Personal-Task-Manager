using Personal_Task_Manager.Models.Enums;

namespace Personal_Task_Manager.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsComplete { get; set; }
        public TimeSpan Timer { get; set; }
        public TaskState State{ get; set; }
        public TaskCategory Category { get; set; }
        public TaskImportance Importance { get; set; }
        public List<TaskCheckList> SubTasks { get; set; } = [];
    }
}
