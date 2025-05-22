using Personal_Task_Manager.Models.Enums;

namespace Personal_Task_Manager.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsComplete => State == TaskState.Complete;
        public TimeSpan Timer { get; set; } = TimeSpan.Zero;
        public TaskState State { get; set; }
        public TaskCategory Category { get; set; }
        public TaskImportance Importance { get; set; }
        public List<TaskCheckList> SubTasks { get; set; } = [];

        public void CompleteTask()
        {
            State = TaskState.Complete;
            foreach (var subTask in SubTasks)
            {
                subTask.IsComplete = true;
            }
            EndDate = DateTime.Now;
        }

        public TimeSpan GetTaskElapsedTime()
        {
            if (IsComplete)
            {
                return EndDate - StartDate;
            }
            else
            {
                if (StartDate > DateTime.Now)
                    return TimeSpan.Zero;

                return DateTime.Now - StartDate;
            }
        }
    }
}
