using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WordPuzzleSolver.Wpf.Converters
{
    public class ActiveViewToColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value?.ToString() == parameter?.ToString() ? Brushes.Green : Brushes.Azure;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
