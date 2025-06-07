using Personal_Task_Manager.Models.Enums;
using Personal_Task_Manager.ViewModel;
using System.Diagnostics.CodeAnalysis;

namespace Personal_Task_Manager.Models
{
    public class TaskItem : BaseViewModel, IObserver<TaskItem>, IEqualityComparer<TaskItem>
    {
        private string title = string.Empty;
        private string description = string.Empty;
        private DateTime? dueDate;
        private DateTime startDate;
        private DateTime endDate;
        private TimeSpan timer = TimeSpan.Zero;
        private TaskState state;
        private TaskCategory category;
        private TaskImportance importance;
        private List<TaskCheckList> subTasks = [];

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public DateTime? DueDate
        {
            get => dueDate;
            set => SetProperty(ref dueDate, value);
        }

        public DateTime StartDate
        {
            get => startDate;
            set => SetProperty(ref startDate, value);
        }

        public DateTime EndDate
        {
            get => endDate;
            set => SetProperty(ref endDate, value);
        }

        public bool IsComplete => State == TaskState.Complete;

        public TimeSpan Timer
        {
            get => timer;
            set => SetProperty(ref timer, value);
        }

        public TaskState State
        {
            get => state;
            set => SetProperty(ref state, value);
        }

        public TaskCategory Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }

        public TaskImportance Importance
        {
            get => importance;
            set => SetProperty(ref importance, value);
        }

        public List<TaskCheckList> SubTasks
        {
            get => subTasks;
            set => SetProperty(ref subTasks, value);
        }

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

        public bool Equals(TaskItem? x, TaskItem? y)
        {
            return x?.Title == y?.Title;
        }

        public int GetHashCode([DisallowNull] TaskItem obj) => HashCode.Combine(obj.Title.GetHashCode());
    }
}
