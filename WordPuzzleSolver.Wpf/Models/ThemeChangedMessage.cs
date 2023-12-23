namespace WordPuzzleSolver.Wpf.Models
{
    internal class ThemeChangedMessage
    {
        public SupportedTheme NewTheme { get; }
        public ThemeChangedMessage(SupportedTheme newTheme)
        {
            NewTheme = newTheme;
        }
    }
}
