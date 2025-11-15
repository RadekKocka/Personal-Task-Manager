using Personal_Task_Manager.Models;
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
    /// Interaction logic for AddSubTaskWindow.xaml
    /// </summary>
    public partial class AddSubTaskWindow : Window
    {
        public AddSubTaskWindow(TaskItemViewModel viewModel) : base()
        {
            Owner = App.Current.MainWindow;
            DataContext = new AddSubTaskItemViewModel(GetDialogResult);
            InitializeComponent();
        }

        public TaskCheckList SubTask { get; private set; }

        private TaskCheckList GetDialogResult(Boolean isCanceled)
        {
            if (!isCanceled && DataContext is AddSubTaskItemViewModel vm)
            {
                SubTask = vm.CreatedSubTask;
            }

            DialogResult = !isCanceled;
            return SubTask;
        }
    }
}
