using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Personal_Task_Manager.Dictionaries.Converters
{
    internal class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is not null && parameter is string param)
            {
                if (param.Equals("Inverse", StringComparison.InvariantCultureIgnoreCase))
                {
                    return value == null ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                }
            }
            return value == null ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
