using Personal_Task_Manager.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Personal_Task_Manager.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private String searchText;
        private ObservableCollection<TaskItem> tasks;
        private ICollectionView tasksView;

        public String SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
                TasksView.Refresh();
            }
        }
        public MainViewModel()
        {
            Tasks = DummyData.DummySeeder.GetDummyTasks();

            TasksView = CollectionViewSource.GetDefaultView(Tasks);
            TasksView.Filter = FilterTasks;
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
    }
}
