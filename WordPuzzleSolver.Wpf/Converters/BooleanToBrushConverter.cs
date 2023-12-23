using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WordPuzzleSolver.Wpf.Converters;

public class BooleanToBrushConverter : IValueConverter
{

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? Brushes.SkyBlue : Brushes.White;
        }

        return Binding.DoNothing;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}