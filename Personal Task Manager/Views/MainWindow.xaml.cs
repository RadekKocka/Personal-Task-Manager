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
                // Check if the click is on an empty space
                var clickedItem = VisualTreeHelper.HitTest(this, e.GetPosition(this));
                if (clickedItem?.VisualHit is not ListBoxItem element)
                {
                    // Set SelectedTask to null
                    if (DataContext is ViewModel.MainViewModel viewModel)
                    {
                        viewModel.SelectedTask = null!;
                    }
                }
            }
        }
    }
}