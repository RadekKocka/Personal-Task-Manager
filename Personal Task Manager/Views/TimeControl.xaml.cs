using Personal_Task_Manager.Models;
using Personal_Task_Manager.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Personal_Task_Manager.Views
{
    /// <summary>
    /// Interaction logic for TimeControl.xaml
    /// </summary>
    public partial class TimeControl : UserControl
    {
        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register(
                nameof(Task),
                typeof(TaskItem),
                typeof(TimeControl),
                new PropertyMetadata(null, OnTaskChanged));

        public TaskItem Task
        {
            get => (TaskItem)GetValue(TaskProperty);
            set => SetValue(TaskProperty, value);
        }
        public TimeControl()
        {
            InitializeComponent();
        }

        public TimeControl(TaskItem task) : this()
        {
            Task = task;
        }

        private static void OnTaskChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TimeControl)d;
            if (e.NewValue is TaskItemViewModel task)
                control.DataContext = new TimeControlViewModel(task);
            else
                control.DataContext = null;
        }
    }
}
