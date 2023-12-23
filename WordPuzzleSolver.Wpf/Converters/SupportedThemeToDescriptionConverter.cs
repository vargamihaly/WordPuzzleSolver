using System;
using System.Globalization;
using System.Windows.Data;

namespace WordPuzzleSolver.Wpf.Converters;

public class SupportedThemeToDescriptionConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is SupportedTheme enumValue)
        {
            return  enumValue.GetDescription();
        }

        return null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}