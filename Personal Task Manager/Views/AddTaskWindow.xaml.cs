using Personal_Task_Manager.Models;
using Personal_Task_Manager.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace Personal_Task_Manager.Views
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window, IDisposable
    {
        public AddTaskViewModel ViewModel { get; }
        private AddTaskWindow(Window owner, AddTaskViewModel addTaskViewModel)
        {
            InitializeComponent();
            ViewModel = addTaskViewModel;
            DataContext = ViewModel;
            Owner = owner;
            ViewModel.TaskCreated += OnTaskSaved;
        }

        public static AddTaskWindow CreateWindow(ObservableCollection<TaskItem> taskItems, TaskItem taskItem, Window owner)
        {
            var vm = new AddTaskViewModel(taskItems, taskItem);
            return new AddTaskWindow(owner, vm);
        }

        public static AddTaskWindow CreateWindow(ObservableCollection<TaskItem> taskItems, Window owner)
        {
            var vm = new AddTaskViewModel(taskItems);
            return new AddTaskWindow(owner, vm);
        }

        private void OnTaskSaved(object? sender, TaskCreatedArgs e)
        {
            if (e.CloseWindow)
            {
                DialogResult = true;
            }
        }

        public void Dispose()
        {
            ViewModel.TaskCreated -= OnTaskSaved;
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
