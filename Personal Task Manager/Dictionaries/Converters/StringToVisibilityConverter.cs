using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Personal_Task_Manager.Dictionaries.Converters
{
    class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue)
            {
                if (parameter is null)
                    return string.IsNullOrEmpty(strValue) ? Visibility.Visible : Visibility.Collapsed;

                if (parameter is string strParameter && strParameter == "Inverse")
                    return string.IsNullOrEmpty(strValue) ? Visibility.Collapsed : Visibility.Visible;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
