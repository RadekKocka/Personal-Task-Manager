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
            ViewModel.TaskEntryFinished += OnTaskSaved;
        }

        public static AddTaskWindow EditTask(ObservableCollection<TaskItemViewModel> taskItems, TaskItemViewModel taskItem, Window owner)
        {
            var vm = new AddTaskViewModel(taskItems, taskItem);
            return new AddTaskWindow(owner, vm);
        }

        public static AddTaskWindow CreateTask(ObservableCollection<TaskItemViewModel> taskItems, Window owner)
        {
            var vm = new AddTaskViewModel(taskItems);
            return new AddTaskWindow(owner, vm);
        }

        private void OnTaskSaved(object? sender, bool canceled)
        {
            DialogResult = !canceled;
        }

        public void Dispose()
        {
            ViewModel.TaskEntryFinished -= OnTaskSaved;
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                DragMove();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Dispose();
        }
    }
}
