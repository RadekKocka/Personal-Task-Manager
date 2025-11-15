using Personal_Task_Manager.DataServices;
using Personal_Task_Manager.Models;
using Personal_Task_Manager.ViewModel;
using System.Collections.ObjectModel;
using System.IO;
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
        private readonly IDataProvider _dataProvider;
        public MainWindow()
        {
            InitializeComponent();
            _dataProvider = new JsonFileProvider();
            try
            {
                _viewModel = new MainViewModel(_dataProvider);
                DataContext = _viewModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown();
            }
        }

        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HitTestResult clickedItem = VisualTreeHelper.HitTest(this, e.GetPosition(this));
            if (clickedItem?.VisualHit is not FrameworkElement element || element.DataContext is not TaskItem)
            {
                _viewModel.SelectedTask = null!;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (_viewModel is not null && _dataProvider is not null)
            {
                try
                {
                    _dataProvider.SaveData(_viewModel.Tasks.Select(x => x.Model).ToList());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}