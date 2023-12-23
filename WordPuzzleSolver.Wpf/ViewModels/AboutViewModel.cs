using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using WordPuzzleSolver.Common.Core;

namespace WordPuzzleSolver.Wpf.ViewModels;

public class AboutViewModel : ObservableObject, ISingleton
{
    public IRelayCommand OpenGitHubCommand { get; } = new RelayCommand(OpenGitHub);
    public IRelayCommand SendEmailCommand { get; } = new RelayCommand(SendEmail);

    private static void OpenGitHub()
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "https://https://github.com/vargamihaly",
            UseShellExecute = true,
        });
    }

    private static void SendEmail()
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "mailto:vmisi20@gmail.com",
            UseShellExecute = true,
        });
    }
}