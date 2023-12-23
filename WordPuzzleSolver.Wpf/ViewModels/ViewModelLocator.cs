using Microsoft.Extensions.DependencyInjection;

namespace WordPuzzleSolver.Wpf.ViewModels;

public class ViewModelLocator
{
    public MainWindowViewModel MainWindowViewModel => App.Current.ServiceProvider.GetRequiredService<MainWindowViewModel>();
    public SolverViewModel SolverViewModel => App.Current.ServiceProvider.GetRequiredService<SolverViewModel>();
    public SettingsViewModel SettingsViewModel => App.Current.ServiceProvider.GetRequiredService<SettingsViewModel>();
}
