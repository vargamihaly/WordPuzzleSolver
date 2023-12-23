using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Windows.Input;
using WordPuzzleSolver.Common.Core;
using WordPuzzleSolver.Wpf.Services;

namespace WordPuzzleSolver.Wpf.ViewModels
{
    public class MainWindowViewModel : ObservableObject, ITransient
    {
        private ObservableObject currentViewModel;
        private string activeViewName;
        private Syncfusion.SfSkinManager.Theme theme;

        private readonly SolverViewModel solverViewModel;
        private readonly SettingsViewModel settingsViewModel;
        private readonly AboutViewModel aboutViewModel;
        private readonly HowToGuideViewModel howToGuideViewModel;

        public MainWindowViewModel(
            ISettingsService settingsService,
            SolverViewModel solverViewModel,
            SettingsViewModel settingsViewModel,
            AboutViewModel aboutViewModel,
            HowToGuideViewModel howToGuideViewModel)
        {
            this.solverViewModel = solverViewModel;
            this.settingsViewModel = settingsViewModel;
            this.aboutViewModel = aboutViewModel;
            this.howToGuideViewModel = howToGuideViewModel;

            InitializeViewModels(settingsService);
            RegisterMessages();
        }

        public ObservableObject CurrentViewModel
        {
            get => currentViewModel;
            set => SetProperty(ref currentViewModel, value);
        }

        public string ActiveViewName
        {
            get => activeViewName;
            private set => SetProperty(ref activeViewName, value);
        }

        public Syncfusion.SfSkinManager.Theme Theme
        {
            get => theme;
            private set => SetProperty(ref theme, value);
        }

        public ICommand NavigateCommand => new RelayCommand<NavigationDestination>(ExecuteNavigateCommand);

        private void InitializeViewModels(ISettingsService settingsService)
        {
            Theme = new Syncfusion.SfSkinManager.Theme(settingsService.GetCurrentTheme().ToString());
            CurrentViewModel = solverViewModel;
            ActiveViewName = "Solver";
        }

        private void RegisterMessages()
        {
            WeakReferenceMessenger.Default.Register<ThemeChangedMessage>(this, OnThemeChanged);
        }

        private void OnThemeChanged(object recipient, ThemeChangedMessage msg)
        {
            Theme = new Syncfusion.SfSkinManager.Theme(msg.NewTheme.ToString());
        }

        private void ExecuteNavigateCommand(NavigationDestination destination)
        {
            ActiveViewName = destination.ToString();
            CurrentViewModel = GetViewModelForDestination(destination);
        }

        private ObservableObject GetViewModelForDestination(NavigationDestination destination)
        {
            return destination switch
            {
                NavigationDestination.Solver => solverViewModel,
                NavigationDestination.Settings => settingsViewModel,
                NavigationDestination.About => aboutViewModel,
                NavigationDestination.HowToGuide => howToGuideViewModel,
                _ => throw new ArgumentException("Unknown navigation destination", nameof(destination)),
            };
        }
    }
}
