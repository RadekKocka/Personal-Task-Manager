using Personal_Task_Manager.Models.Enums;
using Personal_Task_Manager.ViewModel;
using Personal_Task_Manager.ViewModel.Commands;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace Personal_Task_Manager.Models
{
    public class TaskItem
    {
        #region Properties
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; } = null;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsComplete => State == TaskState.Complete;
        public TaskState State {  get; set; }
        public TaskCategory Category {  get; set; }
        public TaskImportance Importance {  get; set; }
        public List<TaskCheckList> SubTasks { get; set; } = [];
        #endregion

        public bool Equals(TaskItem? x, TaskItem? y)
        {
            return x?.Title == y?.Title;
        }

        public int GetHashCode([DisallowNull] TaskItem obj) => HashCode.Combine(obj.Title.GetHashCode());
    }
}
