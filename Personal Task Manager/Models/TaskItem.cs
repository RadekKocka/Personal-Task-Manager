using Personal_Task_Manager.Models.Enums;
using Personal_Task_Manager.ViewModel;
using System.Collections.ObjectModel;

namespace Personal_Task_Manager.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Deleted { get; set; }
        public bool Archived { get; set; }
        public TimeSpan TaskElapsedTime { get; set; }
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
        }

        public TimeSpan GetElapsedTime()
        {
            if (State == TaskState.InProgress)
            {
                return DateTime.Now - StartDate;
            }
            else
            {
                if (EndDate < StartDate)
                    return TimeSpan.Zero;

                return EndDate - StartDate;
            }
        }
    }
}
