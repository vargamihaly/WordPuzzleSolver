using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace WordPuzzleSolver.Wpf.Models;

public enum SupportedLanguage
{
    [Description("English")]
    [LanguageCode("en")]
    English,

    [Description("Hungarian")]
    [LanguageCode("hu")]
    Hungarian,
}

[AttributeUsage(AttributeTargets.Field)]
public class LanguageCodeAttribute : Attribute
{
    public string Code { get; }

    public LanguageCodeAttribute(string code)
    {
        Code = code;
    }
}

public static class SupportedLanguageExtensions
{
    public static string GetDescription(this SupportedLanguage language)
    {
        return language.GetType().GetMember(language.ToString()).First()
            .GetCustomAttribute<DescriptionAttribute>()?.Description ?? string.Empty;
    }

    public static string GetLanguageCode(this SupportedLanguage language)
    {
        return language.GetType().GetMember(language.ToString()).First()
            .GetCustomAttribute<LanguageCodeAttribute>()?.Code ?? string.Empty;
    }
}