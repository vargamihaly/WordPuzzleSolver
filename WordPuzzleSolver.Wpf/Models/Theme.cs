using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace WordPuzzleSolver.Wpf.Models;

public enum SupportedTheme
{
    [Description("Windows 11 Dark")]
    [ThemeCode("Windows11Dark")]
    Windows11Dark,

    [Description("Material Light")]
    [ThemeCode("MaterialLight")]
    MaterialLight,
}

[AttributeUsage(AttributeTargets.Field)]
public class ThemeCodeAttribute : Attribute
{
    public string Code { get; }

    public ThemeCodeAttribute(string code)
    {
        Code = code;
    }
}

public static class SupportedThemeExtensions
{
    public static string GetDescription(this SupportedTheme theme)
    {
        return theme.GetType().GetMember(theme.ToString()).First()
            .GetCustomAttribute<DescriptionAttribute>()?.Description ?? string.Empty;
    }

    public static string GetThemeCode(this SupportedTheme theme)
    {
        return theme.GetType().GetMember(theme.ToString()).First()
            .GetCustomAttribute<ThemeCodeAttribute>()?.Code ?? string.Empty;
    }
}