using WordPuzzleSolver.Wpf.ViewModels;

namespace WordPuzzleSolver.Wpf.Views;

public partial class AboutView
{
    public AboutView()
    {
        InitializeComponent();
        DataContext = new AboutViewModel();
    }
}