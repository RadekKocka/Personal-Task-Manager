using Personal_Task_Manager.Models.Enums;

namespace Personal_Task_Manager.Models
{
    public class TaskItem : IObserver<TaskItem>
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
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
            EndDate = DateTime.Now;
        }

        public void CompleteSubTasks()
        {
            foreach (var subTask in SubTasks)
            {
                subTask.IsComplete = true;
            }
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

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(TaskItem value)
        {
            if (SubTasks.All(subtask => subtask.IsComplete))
            {
                CompleteTask();
            }
        }
    }
}
