using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Personal_Task_Manager.Controls
{
    public class WatermarkTextBox : TextBox
    {
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register(nameof(Watermark), typeof(string), typeof(WatermarkTextBox), new PropertyMetadata(string.Empty));

        public string Watermark
        {
            get => (string)GetValue(WatermarkProperty);
            set => SetValue(WatermarkProperty, value);
        }

        public SolidColorBrush WatermarkForeground
        {
            get { return (SolidColorBrush)GetValue(WatermarkForegroundProperty); }
            set { SetValue(WatermarkForegroundProperty, value); }
        }

        public static readonly DependencyProperty WatermarkForegroundProperty =
            DependencyProperty.Register(nameof(WatermarkForeground), typeof(SolidColorBrush), typeof(WatermarkTextBox), new PropertyMetadata(Brushes.Gray));

        static WatermarkTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WatermarkTextBox), new FrameworkPropertyMetadata(typeof(WatermarkTextBox)));
        }
    }
}
