using WordPuzzleSolver.Common.Core;

namespace WordPuzzleSolver.Wpf.Models;

public class Settings : ISingleton
{
    public int MinWordLength { get; set; }
    public int MaxWordLength { get; set; }
    public int BoardSize { get; set; }

    public SupportedLanguage CurrentLanguage { get; set; } = SupportedLanguage.Hungarian;

    public SupportedTheme CurrentTheme { get; set; } = SupportedTheme.Windows11Dark;
}