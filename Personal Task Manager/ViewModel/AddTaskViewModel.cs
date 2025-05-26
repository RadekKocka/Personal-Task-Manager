using Personal_Task_Manager.Models;
using Personal_Task_Manager.Models.Enums;
using Personal_Task_Manager.Services;
using Personal_Task_Manager.ViewModel.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Personal_Task_Manager.ViewModel
{
    public class AddTaskViewModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<TaskImportance> Importances { get; }
        public List<TaskCategory> Categories { get; }

        public TaskImportance? SelectedImportance { get; set; }
        public TaskCategory? SelectedCategory { get; set; }
        public DateTime? DueDate { get; set; }

        private List<TaskCheckList> subTasks = new List<TaskCheckList>();

        private ObservableCollection<TaskItem> _taskItems;

        public event EventHandler<TaskCreatedArgs>? TaskCreated;

        public ICommand AddTaskCommand => new RelayCommand(_ => AddTask(false), _ => CanAddTask());
        public ICommand AddTaskAndCloseCommand => new RelayCommand(_ => AddTask(true), _ => CanAddTask());
        public ICommand CancelCommand => new RelayCommand(_ => Cancel());

        public AddTaskViewModel(ObservableCollection<TaskItem> taskItems)
        {
            Categories = Enum.GetValues<TaskCategory>().ToList();
            Importances = Enum.GetValues<TaskImportance>().ToList();

            SelectedCategory = null;
            SelectedImportance = null;

            _taskItems = taskItems;
        }

        public void Cancel()
        {
            OnTaskCreated(true);
        }

        public void AddTask(bool closeWindow)
        {
            var task = TaskItemBuilder.Create()
                .WithTitle(Title ?? string.Empty)
                .WithDescription(Description ?? string.Empty)
                .WithDueDate(DueDate)
                .WithState(TaskState.InProgress)
                .WithCategory(SelectedCategory!.Value)
                .WithImportance(SelectedImportance!.Value)
                .WithSubTasks(subTasks)
                .Build();

            _taskItems.Add(task);

            OnTaskCreated(closeWindow);
        }

        public void OnTaskCreated(bool closeWindow)
        {
            TaskCreated?.Invoke(this, new TaskCreatedArgs(closeWindow));
        }

        private bool CanAddTask()
        {
            return !string.IsNullOrWhiteSpace(Title) 
                   && !_taskItems.Any(x => x.Title.Equals(Title, StringComparison.InvariantCultureIgnoreCase))
                   && SelectedCategory is not null
                   && SelectedImportance is not null;
        }
    }

    public class TaskCreatedArgs
    {
        public bool CloseWindow { get; set; }
        public TaskCreatedArgs(bool closeWindow)
        {
            CloseWindow = closeWindow;
        }
    }
}
