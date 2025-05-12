using Personal_Task_Manager.Models;
using Personal_Task_Manager.ViewModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Personal_Task_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
        }

        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HitTestResult clickedItem = VisualTreeHelper.HitTest(this, e.GetPosition(this));
            if (clickedItem?.VisualHit is not FrameworkElement element || element.DataContext is not TaskItem)
            {
                _viewModel.SelectedTask = null!;
            }
        }
    }
}