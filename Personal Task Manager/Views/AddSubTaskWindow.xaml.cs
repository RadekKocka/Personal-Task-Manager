using Personal_Task_Manager.Models;
using Personal_Task_Manager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for AddSubTaskWindow.xaml
    /// </summary>
    public partial class AddSubTaskWindow : Window
    {
        private AddSubTaskItemViewModel SubTaskViewModel { get; }
        private AddSubTaskWindow() : base()
        {
            Owner = App.Current.MainWindow;
            SubTaskViewModel = new AddSubTaskItemViewModel();
            SubTaskViewModel.CloseEventHandler += CloseWindow;
            DataContext = SubTaskViewModel;
            InitializeComponent();
        }

        private AddSubTaskWindow(TaskCheckList taskCheckList) : this()
        {
            SubTaskViewModel.TaskDescription = taskCheckList?.Description ?? String.Empty;
        }

        public static Boolean CreateSubTask(out String description)
        {
            return EditSubTask(null, out description);
        }

        public static Boolean EditSubTask(TaskCheckList subTask, out String description)
        {
            var window = new AddSubTaskWindow(subTask);
            var result = window.ShowDialog();
            description = window.SubTaskViewModel.TaskDescription;
            return result ?? false;
        }

        private void CloseWindow(Boolean dialogResult)
        {
            SubTaskViewModel.CloseEventHandler -= CloseWindow;
            DialogResult = dialogResult;
            this.Close();
        }
    }
}
