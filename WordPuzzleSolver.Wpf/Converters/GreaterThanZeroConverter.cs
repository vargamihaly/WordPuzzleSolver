using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System;

namespace WordPuzzleSolver.Wpf.Converters
{
    public class GreaterThanZeroConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue > 0 ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}