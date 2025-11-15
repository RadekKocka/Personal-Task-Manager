using Personal_Task_Manager.DataServices;
using Personal_Task_Manager.DummyData;
using Personal_Task_Manager.Models;
using Personal_Task_Manager.Models.Enums;
using Personal_Task_Manager.Models.Helpers;
using Personal_Task_Manager.Services;
using Personal_Task_Manager.ViewModel.Commands;
using Personal_Task_Manager.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Personal_Task_Manager.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Fields
        private string searchText;
        private ObservableCollection<TaskItemViewModel> tasks;
        private ICollectionView tasksView;
        private TaskItemViewModel selectedTask;
        private bool showCompletedTasks = false;
        private TaskState taskStateFlags;
        private SortingEnum _selectedSortingItem;
        #endregion

        #region Constructor
        public MainViewModel(IDataProvider dataProvider)
        {
            ArgumentNullException.ThrowIfNull(dataProvider);
            Tasks = new ObservableCollection<TaskItemViewModel>(dataProvider.LoadData().Select(x => new TaskItemViewModel(x)));
            //Tasks = DummySeeder.GetDummyTasks();
            UpdateTaskFlags();
            TasksView = GetTasksInProgressView(Tasks);
        }
        #endregion

        #region Properties

        public string SearchText
        {
            get => searchText;
            set
            {
                SetProperty(ref searchText, value);
                TasksView.Refresh();
            }
        }

        public ICollectionView TasksView
        {
            get => tasksView;
            set => SetProperty(ref tasksView, value);
        }

        public ObservableCollection<TaskItemViewModel> Tasks
        {
            get => tasks;
            set => SetProperty(ref tasks, value);
        }

        public TaskItemViewModel SelectedTask
        {
            get => selectedTask;
            set
            {
                // set null so storyboard can happen
                selectedTask = null;
                OnPropertyChanged(nameof(SelectedTask));
                selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
                OnPropertyChanged(nameof(TaskElapsedTime));
            }
        }

        public DateTime CurrentTime => DateTime.Now;

        public string TaskElapsedTime => (SelectedTask?.GetTaskElapsedTime() ?? TimeSpan.Zero).ToString(@"hh\:mm\:ss");

        public bool ShowCompletedTasks
        {
            get => showCompletedTasks;
            set
            {
                SetProperty(ref showCompletedTasks, value);
                UpdateTaskFlags();
                TasksView.Refresh();
            }
        }

        public List<KeyValuePair<SortingEnum, String>> SortItems => Enum.GetValues(typeof(SortingEnum)).Cast<SortingEnum>().Select(x => EnumDescriptionExtension.GetValueAndDescription(x)).ToList();

        public SortingEnum SelectedSortingItem
        {
            get => _selectedSortingItem;
            set
            {
                SetProperty(ref _selectedSortingItem, value);
                TasksView.SortDescriptions.Clear();
                SetSort(_selectedSortingItem);
                TasksView.Refresh();
            }
        }

        #endregion

        #region Commands
        public ICommand DeleteTaskCommand => new RelayCommand<TaskItemViewModel>(DeleteTask);
        public ICommand AddTaskCommand => new RelayCommand(AddNewTask);

        public ICommand CompleteTaskCommand => new RelayCommand<TaskItemViewModel>(CompleteTask, CanCompleteTask);

        public ICommand EditTaskCommand => new RelayCommand<TaskItemViewModel>(EditTask);

        #endregion

        #region Methods
        private void DeleteTask(TaskItemViewModel item)
        {
            ArgumentNullException.ThrowIfNull(item);

            var message = $"Delete task {item.Title}?";
            var caption = "Delete task";
            if (MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Tasks.Remove(item);
            }
        }

        private bool FilterTasks(object obj)
        {
            bool searchFilter = true;
            if (obj is TaskItemViewModel task)
            {
                bool taskState = (taskStateFlags & task.State) == task.State;

                searchFilter = (string.IsNullOrWhiteSpace(SearchText) ||
                       task.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                       (task.Description?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false));
                return searchFilter && taskState;
            }
            return true;
        }

        private void CompleteTask(TaskItemViewModel taskItem)
        {
            ArgumentNullException.ThrowIfNull(taskItem);
            var message = $"Complete task {taskItem.Title} and it's subsequent subtasks?";
            var caption = "Complete task";
            if (MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                taskItem.CompleteSubTasks();
                taskItem.CompleteTask();
            }
            tasksView.Refresh();
        }

        private bool CanCompleteTask(TaskItemViewModel taskItem)
        {
            return taskItem != null && !taskItem.IsComplete;
        }

        private void AddNewTask(object? obj)
        {
            var addTaskWindow = AddTaskWindow.CreateTask(Tasks, App.Current.MainWindow);
            addTaskWindow.ShowDialog();
        }

        private void EditTask(TaskItemViewModel item)
        {
            var editTaskWindow = AddTaskWindow.EditTask(Tasks, item, App.Current.MainWindow);
            editTaskWindow.ShowDialog();
            tasksView.Refresh();
        }

        private ICollectionView GetTasksInProgressView(ObservableCollection<TaskItemViewModel> taskItems)
        {
            tasksView = CollectionViewSource.GetDefaultView(taskItems);
            tasksView.Filter = FilterTasks;
            tasksView.SortDescriptions.Add(new SortDescription(nameof(TaskItem.DueDate), ListSortDirection.Ascending));
            return tasksView;
        }

        private void UpdateTaskFlags()
        {
            taskStateFlags = TaskState.All;
            if (!ShowCompletedTasks)
                taskStateFlags &= ~TaskState.Complete;
        }

        private void SetSort(SortingEnum sortingEnum)
        {
            if (TasksView is not ListCollectionView listCollectionView)
                return;

            switch (sortingEnum)
            {
                case SortingEnum.ByDueDateAscending:
                    listCollectionView.CustomSort = new DueDateComparer(isAscending: true);
                    break;
                case SortingEnum.ByDueDateDescending:
                    listCollectionView.CustomSort = new DueDateComparer(isAscending: false);
                    break;
                case SortingEnum.ByImportanceAscending:
                    listCollectionView.CustomSort = new ImportanceComparer(isAscending: true);
                    break;
                case SortingEnum.ByImportanceDescending:
                    listCollectionView.CustomSort = new ImportanceComparer(isAscending: false);
                    break;
                default:
                    TasksView.SortDescriptions.Add(new SortDescription(nameof(TaskItem.DueDate), ListSortDirection.Ascending));
                    break;
            }
        }

        #endregion
    }
}
