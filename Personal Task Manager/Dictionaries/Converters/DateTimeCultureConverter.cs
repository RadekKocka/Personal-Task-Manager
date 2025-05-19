using System.Globalization;
using System.Windows.Data;

namespace Personal_Task_Manager.Dictionaries.Converters
{
    public class DateTimeCultureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null && value is DateTime datetime)
            {
                return datetime.ToString(CultureInfo.CurrentCulture);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
