namespace WordPuzzleSolver.Wpf.Models;

public class LanguageChangedMessage
{
    public SupportedLanguage NewLanguage { get; }
    public LanguageChangedMessage(SupportedLanguage newLanguage)
    {
        NewLanguage = newLanguage;
    }
}