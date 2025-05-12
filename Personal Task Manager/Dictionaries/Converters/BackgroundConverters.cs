using Personal_Task_Manager.Models.Enums;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Personal_Task_Manager.Dictionaries.Converters
{
    public class ImportanceBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string importanceString;
            if (value is TaskImportance importance)
            {
                importanceString = importance switch
                {
                    TaskImportance.Low => "LowImportance",
                    TaskImportance.Medium => "MediumImportance",
                    TaskImportance.High => "HighImportance",
                    TaskImportance.Critical => "CriticalImportance",
                    _ => throw new ArgumentOutOfRangeException(nameof(importance), importance, "Invalid TaskImportance value."),
                };

                if (Application.Current.Resources[importanceString] is SolidColorBrush brush)
                    return brush;
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CategoryBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TaskCategory category)
            {
                return category switch
                {
                    TaskCategory.Work => new SolidColorBrush(Color.FromRgb(255, 223, 186)), // Light Orange
                    TaskCategory.Personal => new SolidColorBrush(Color.FromRgb(186, 255, 223)), // Light Green
                    TaskCategory.Home => new SolidColorBrush(Color.FromRgb(186, 223, 255)), // Light Blue
                    TaskCategory.Health => new SolidColorBrush(Color.FromRgb(255, 186, 223)), // Light Pink
                    TaskCategory.Finance => new SolidColorBrush(Color.FromRgb(223, 255, 186)), // Light Yellow
                    TaskCategory.Shopping => new SolidColorBrush(Color.FromRgb(223, 186, 255)), // Light Purple
                    TaskCategory.Social => new SolidColorBrush(Color.FromRgb(255, 186, 186)), // Light Red
                    TaskCategory.Family => new SolidColorBrush(Color.FromRgb(186, 255, 186)), // Light Mint
                    TaskCategory.Travel => new SolidColorBrush(Color.FromRgb(186, 223, 223)), // Light Teal
                    TaskCategory.Errands => new SolidColorBrush(Color.FromRgb(223, 186, 223)), // Light Lavender
                    TaskCategory.Education => new SolidColorBrush(Color.FromRgb(255, 255, 186)), // Light Lemon
                    TaskCategory.Hobbies => new SolidColorBrush(Color.FromRgb(186, 186, 255)), // Light Periwinkle
                    TaskCategory.Anniversaries => new SolidColorBrush(Color.FromRgb(255, 200, 200)), // Soft Red
                    TaskCategory.Projects => new SolidColorBrush(Color.FromRgb(200, 200, 255)), // Soft Blue
                    _ => Brushes.Transparent,
                };
            }
            return Brushes.Transparent;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


