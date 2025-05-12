using Personal_Task_Manager.ViewModel;
using System.Windows;

namespace Personal_Task_Manager.Views
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        private AddTaskWindow()
        {
            InitializeComponent();
            DataContext = new AddTaskViewModel();
        }

        public static void AddTask(Window parentWindow)
        {
            AddTaskWindow window = new();
            window.ShowDialog();
        }
    }
}
