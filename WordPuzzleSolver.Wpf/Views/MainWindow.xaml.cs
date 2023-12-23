using Syncfusion.SfSkinManager;
using System.Windows.Input;
using WordPuzzleSolver.Common.Core;
using WordPuzzleSolver.Wpf.Services;
using WordPuzzleSolver.Wpf.ViewModels;

namespace WordPuzzleSolver.Wpf.Views;

public partial class MainWindow : ITransient
{
    public MainWindow(ISettingsService settingsService,SolverViewModel solverViewModel, SettingsViewModel settingsViewModel, AboutViewModel aboutViewModel, HowToGuideViewModel howToGuideViewModel)
    {
        SfSkinManager.ApplyStylesOnApplication = true;
        InitializeComponent();
        DataContext = new MainWindowViewModel(settingsService, solverViewModel, settingsViewModel, aboutViewModel, howToGuideViewModel);
    }
    
    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }
}