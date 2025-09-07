using Personal_Task_Manager.DataServices;
using Personal_Task_Manager.DummyData;
using Personal_Task_Manager.Models;
using Personal_Task_Manager.Models.Enums;
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
        private ObservableCollection<TaskItem> tasks;
        private ICollectionView tasksView;
        private TaskItem selectedTask;
        private TaskTimerService timer;
        private bool showCompletedTasks = false;
        private TaskState taskStateFlags;
        #endregion

        #region Constructor
        public MainViewModel(IDataProvider dataProvider)
        {
            ArgumentNullException.ThrowIfNull(dataProvider);
            Tasks = new ObservableCollection<TaskItem>(dataProvider.LoadData());
            //Tasks = DummySeeder.GetDummyTasks();
            UpdateTaskFlags();
            TasksView = GetTasksInProgressView(Tasks);
            timer = new TaskTimerService(UpdateTimes);
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

        public ObservableCollection<TaskItem> Tasks
        {
            get => tasks;
            set => SetProperty(ref tasks, value);
        }

        public TaskItem SelectedTask
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
        #endregion

        #region Commands
        public ICommand DeleteTaskCommand => new RelayCommand<TaskItem>(DeleteTask);
        public ICommand AddTaskCommand => new RelayCommand(AddNewTask);

        public ICommand CompleteTaskCommand => new RelayCommand<TaskItem>(CompleteTask, CanCompleteTask);

        public ICommand EditTaskCommand => new RelayCommand<TaskItem>(EditTask);

        #endregion

        #region Methods
        private void DeleteTask(TaskItem item)
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
            if (obj is TaskItem task)
            {
                bool taskState = (taskStateFlags & task.State) == task.State;

                searchFilter = (string.IsNullOrWhiteSpace(SearchText) ||
                       task.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                       (task.Description?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false));
                return searchFilter && taskState;
            }
            return true;
        }

        private void UpdateTimes()
        {
            OnPropertyChanged(nameof(CurrentTime));
            if (SelectedTask != null)
            {
                OnPropertyChanged(nameof(TaskElapsedTime));
            }
        }

        private void CompleteTask(TaskItem taskItem)
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

        private bool CanCompleteTask(TaskItem taskItem)
        {
            return taskItem != null && !taskItem.IsComplete;
        }

        private void AddNewTask(object? obj)
        {
            var addTaskWindow = AddTaskWindow.CreateTask(Tasks, App.Current.MainWindow);
            addTaskWindow.ShowDialog();
        }

        private void EditTask(TaskItem item)
        {
            var editTaskWindow = AddTaskWindow.EditTask(Tasks, item, App.Current.MainWindow);
            editTaskWindow.ShowDialog();
            tasksView.Refresh();
        }

        private ICollectionView GetTasksInProgressView(ObservableCollection<TaskItem> taskItems)
        {
            tasksView = CollectionViewSource.GetDefaultView(taskItems);
            tasksView.Filter = FilterTasks;
            tasksView.SortDescriptions.Add(new SortDescription(nameof(TaskItem.IsComplete), ListSortDirection.Ascending));
            return tasksView;
        }

        private void UpdateTaskFlags()
        {
            taskStateFlags = TaskState.All;
            taskStateFlags = ShowCompletedTasks ? taskStateFlags &= TaskState.Complete : taskStateFlags &= ~TaskState.Complete;
        }

        #endregion
    }
}
