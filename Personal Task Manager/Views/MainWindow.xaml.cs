using Personal_Task_Manager.Models;
using Personal_Task_Manager.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Personal_Task_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                var clickedItem = VisualTreeHelper.HitTest(this, e.GetPosition(this));
                if (clickedItem?.VisualHit is not ListBoxItem)
                {
                    if (DataContext is MainViewModel viewModel)
                    {
                        viewModel.SelectedTask = null!;
                    }
                }
            }
        }
    }
}