using Personal_Task_Manager.Models;
using Personal_Task_Manager.Models.Enums;
using Personal_Task_Manager.Services;
using Personal_Task_Manager.ViewModel.Commands;
using Personal_Task_Manager.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace Personal_Task_Manager.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Fields
        private string searchText;
        private ObservableCollection<TaskItemViewModel> tasks;
        private ICollectionView tasksView;
        private TaskItemViewModel selectedTask;
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
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
                TasksView.Refresh();
            }
        }

        public ICollectionView TasksView
        {
            get => tasksView;
            private set
            {
                tasksView = value;
                OnPropertyChanged(nameof(TasksView));
            }
        }

        public ObservableCollection<TaskItemViewModel> Tasks
        {
            get => tasks;
            set
            {
                tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        public TaskItemViewModel SelectedTask
        {
            get => selectedTask;
            set
            {
                selectedTask = null;
                OnPropertyChanged(nameof(SelectedTask));
                selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
                if (selectedTask != null)
                {
                    UpdateTaskTime(selectedTask);
                }
            }
        }

        public DateTime CurrentTime => DateTime.Now;

        public String? TaskElapsedTime => new DateTime(SelectedTask?.TaskElapsedTime.Ticks ?? 0).ToLongTimeString();
        #endregion

        #region Commands
        public RelayCommand<TaskItemViewModel> DeleteTaskCommand => new RelayCommand<TaskItemViewModel>(DeleteTask);
        public RelayCommand AddTaskCommand => new RelayCommand(_ =>
        {
            AddTaskWindow.AddTask(Application.Current.MainWindow);
        });

        public RelayCommand<TaskItemViewModel> CompleteTaskCommand => new RelayCommand<TaskItemViewModel>(CompleteTask, CompleteTaskCanExecute);
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
                TasksView.Refresh();
            }
        }

        private void CompleteTask(TaskItemViewModel item)
        {
            item.CompleteTask();
        }

        private bool CompleteTaskCanExecute(TaskItemViewModel taskItem)
        {
            return taskItem != null && taskItem.State is not TaskState.Complete;
        }

        private bool FilterTasks(object obj)
        {
            if (obj is TaskItemViewModel task)
            {
                return string.IsNullOrWhiteSpace(SearchText) ||
                       task.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                       (task.Description?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false);
            }
            return false;
        }

        private void UpdateTaskTime(TaskItemViewModel selectedTask)
        {
            try
            {
                selectedTask.TaskElapsedTime = selectedTask.GetElapsedTime();
                OnPropertyChanged(nameof(TaskElapsedTime));
                OnPropertyChanged(nameof(SelectedTask));
            }
            catch (Exception ex)
            {
                // Todo: make a logger
                Console.WriteLine(ex.Message);
            }
        }

        private void UpdateTimes()
        {
            OnPropertyChanged(nameof(CurrentTime));
            if (SelectedTask == null)
                return;
            UpdateTaskTime(SelectedTask);
        }
        #endregion
    }
}
