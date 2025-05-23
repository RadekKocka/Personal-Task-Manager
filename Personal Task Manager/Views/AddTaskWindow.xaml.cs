using Personal_Task_Manager.Models;
using Personal_Task_Manager.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace Personal_Task_Manager.Views
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        public AddTaskViewModel ViewModel { get; }
        public AddTaskWindow(Window owner, ObservableCollection<TaskItem> taskItems)
        {
            InitializeComponent();
            ViewModel = new AddTaskViewModel(taskItems);
            DataContext = ViewModel;
            Owner = owner;
            ViewModel.TaskCreated += OnTaskCreated;
        }
        private void OnTaskCreated(object? sender, TaskCreatedArgs e)
        {
            if (e.CloseWindow)
            {
                DialogResult = true;
            }
        }
    }
}
