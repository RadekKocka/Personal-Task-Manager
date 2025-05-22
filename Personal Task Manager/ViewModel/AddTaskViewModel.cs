using Personal_Task_Manager.Models;
using Personal_Task_Manager.Models.Enums;
using Personal_Task_Manager.ViewModel.Commands;
using System.Windows.Input;

namespace Personal_Task_Manager.ViewModel
{
    public class AddTaskViewModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<TaskImportance> Importances { get; }
        public List<TaskCategory> Categories { get; }

        public TaskImportance SelectedImportance { get; set; }
        public TaskCategory SelectedCategory { get; set; }
        public DateTime? DueDate { get; set; }

        public TaskItem CreatedTask { get; set; }

        public event Action? TaskCreated;

        public ICommand AddTaskCommand => new RelayCommand(_ => AddTask(), _ => CanAddTask());

        public AddTaskViewModel()
        {
            Categories = Enum.GetValues<TaskCategory>().ToList();
            Importances = Enum.GetValues<TaskImportance>().ToList();

            SelectedImportance = TaskImportance.Medium;
            SelectedCategory = TaskCategory.Work;
        }

        public void AddTask()
        {
            var task = new TaskItem
            {
                Title = Title,
                Description = Description,
                DueDate = DueDate,
                StartDate = DateTime.Now,
                State = TaskState.InProgress,
                Category = SelectedCategory,
                Importance = SelectedImportance
            };
            CreatedTask = task;

            TaskCreated?.Invoke();
        }

        private bool CanAddTask()
        {
            return !string.IsNullOrWhiteSpace(Title);
        }
    }
}
