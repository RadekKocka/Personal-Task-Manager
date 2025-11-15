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
        #region Fields
        private TaskItemViewModel _taskItem;
        #endregion

        #region Properties
        public string Title { get; set; }
        public string Description { get; set; }
        public List<TaskImportance> Importances { get; }
        public List<TaskCategory> Categories { get; }

        public TaskImportance? SelectedImportance { get; set; }
        public TaskCategory? SelectedCategory { get; set; }
        public DateTime? DueDate { get; set; }

        private ObservableCollection<TaskCheckList> SubTasks = [];

        private ObservableCollection<TaskItemViewModel> _taskItems;

        public event EventHandler<bool> TaskEntryFinished;
        #endregion

        #region Commands
        public ICommand SaveTaskCommand => new RelayCommand(_ => SaveTask(), _ => CanAddTask());
        public ICommand CancelCommand => new RelayCommand(_ => Cancel());
        #endregion

        #region Constructor
        public AddTaskViewModel(ObservableCollection<TaskItemViewModel> taskItems)
        {
            Categories = Enum.GetValues<TaskCategory>().ToList();
            Importances = Enum.GetValues<TaskImportance>().ToList();

            if (_taskItem is null)
            {
                SelectedCategory = null;
                SelectedImportance = null;
            }

            _taskItems = taskItems;
        }

        public AddTaskViewModel(ObservableCollection<TaskItemViewModel> taskItems, TaskItemViewModel taskItem)
            : this(taskItems)
        {
            _taskItem = taskItem;
            Title = taskItem.Title;
            Description = taskItem.Description;
            DueDate = taskItem.DueDate;
            SelectedCategory = taskItem.Category;
            SelectedImportance = taskItem.Importance;
            SubTasks = taskItem.SubTasks;
        }
        #endregion

        public void Cancel()
        {
            OnTaskEntryFinished(true);
        }

        public void SaveTask()
        {
            if (_taskItem is not null)
            {
                _taskItem.Title = Title;
                _taskItem.Description = Description ?? string.Empty;
                _taskItem.DueDate = DueDate;
                _taskItem.State = TaskState.InProgress;
                _taskItem.Category = SelectedCategory!.Value;
                _taskItem.Importance = SelectedImportance!.Value;
                _taskItem.SubTasks = SubTasks;
            }
            else
            {
                var task = TaskItemBuilder.Create()
                .WithTitle(Title)
                .WithDescription(Description ?? string.Empty)
                .WithDueDate(DueDate)
                .WithState(TaskState.InProgress)
                .WithCategory(SelectedCategory!.Value)
                .WithImportance(SelectedImportance!.Value)
                .WithSubTasks(SubTasks)
                .Build();

                _taskItems.Add(new TaskItemViewModel(task));
            }

            OnTaskEntryFinished(false);
        }

        public void OnTaskEntryFinished(bool canceled)
        {
            TaskEntryFinished?.Invoke(this, canceled);
        }

        private bool CanAddTask()
        {
            if (_taskItem is not null)
            {
                return !string.IsNullOrWhiteSpace(Title)
                       && SelectedCategory is not null
                       && SelectedImportance is not null;
            }

            return !string.IsNullOrWhiteSpace(Title)
                   && !_taskItems.Any(x => x.Title.Equals(Title, StringComparison.InvariantCultureIgnoreCase))
                   && SelectedCategory is not null
                   && SelectedImportance is not null;
        }

        public void Dispose()
        {
            _taskItems.Clear();
        }
    }
}
