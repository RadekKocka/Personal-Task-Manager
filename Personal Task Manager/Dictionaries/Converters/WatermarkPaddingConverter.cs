using System.Windows;
using System.Windows.Data;

namespace Personal_Task_Manager.Dictionaries.Converters
{
    class WatermarkPaddingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Thickness thickness)
            {
                if (parameter is null || parameter is VerticalAlignment verticalAlignment && verticalAlignment is VerticalAlignment.Top)
                {
                    thickness.Top += 3;
                }
                thickness.Left += 3;
            }
            return thickness;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
