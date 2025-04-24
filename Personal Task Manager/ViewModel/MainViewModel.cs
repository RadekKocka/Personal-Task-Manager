using Personal_Task_Manager.Models;
using Personal_Task_Manager.ViewModel.Commands;
using Personal_Task_Manager.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace Personal_Task_Manager.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Fields
        private string searchText;
        private ObservableCollection<TaskItem> tasks;
        private ICollectionView tasksView;
        #endregion

        #region Constructor
        public MainViewModel()
        {
            Tasks = DummyData.DummySeeder.GetDummyTasks();

            TasksView = CollectionViewSource.GetDefaultView(Tasks);
            TasksView.Filter = FilterTasks;
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

        public ObservableCollection<TaskItem> Tasks
        {
            get => tasks;
            set
            {
                tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }
        #endregion

        #region Commands
        public RelayCommand<TaskItem> DeleteTaskCommand => new RelayCommand<TaskItem>(DeleteTask);
        public RelayCommand AddTaskCommand => new RelayCommand(_ =>
        {
            AddTaskWindow.AddTask(Application.Current.MainWindow);
        });
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
                TasksView.Refresh();
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
        #endregion
    }
}
