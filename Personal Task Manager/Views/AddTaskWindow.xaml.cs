using Personal_Task_Manager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
