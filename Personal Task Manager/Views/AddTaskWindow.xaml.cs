using Personal_Task_Manager.ViewModel;
using System.Windows;

namespace Personal_Task_Manager.Views
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        public AddTaskViewModel ViewModel { get; }
        public AddTaskWindow(Window owner)
        {
            InitializeComponent();
            ViewModel = new AddTaskViewModel();
            DataContext = ViewModel;
            Owner = owner;
            ViewModel.TaskCreated += OnTaskCreated;
        }

        private void OnTaskCreated()
        {
            DialogResult = true;
            Close();
        }
    }
}
