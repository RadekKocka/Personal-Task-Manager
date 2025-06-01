using Personal_Task_Manager.Models;
using Personal_Task_Manager.Services;
using Personal_Task_Manager.ViewModel.Commands;
using Personal_Task_Manager.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

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
        #endregion

        #region Constructor
        public MainViewModel()
        {
            Tasks = DummyData.DummySeeder.GetDummyTasks();

            TasksView = CollectionViewSource.GetDefaultView(Tasks);
            TasksView.Filter = FilterTasks;
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
            if (obj is TaskItem task)
            {
                return string.IsNullOrWhiteSpace(SearchText) ||
                       task.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                       (task.Description?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false);
            }
            return false;
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
        }

        private bool CanCompleteTask(TaskItem taskItem)
        {
            return taskItem != null && !taskItem.IsComplete;
        }

        private void AddNewTask(object? obj)
        {
            var addTaskWindow = AddTaskWindow.CreateWindow(Tasks, App.Current.MainWindow);
            if (addTaskWindow.ShowDialog() is true)
                addTaskWindow.Dispose();
        }

        private void EditTask(TaskItem item)
        {
            var editTaskWindow = AddTaskWindow.CreateWindow(Tasks, item, App.Current.MainWindow);
            if (editTaskWindow.ShowDialog() == true)
                editTaskWindow.Dispose();
            tasksView.Refresh();
        }

        #endregion
    }
}
