using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using WordPuzzleSolver.Common.Core;
using WordPuzzleSolver.Wpf.Services;

namespace WordPuzzleSolver.Wpf.ViewModels;

public class SettingsViewModel : ObservableObject , ISingleton
{
    private const int MinWordLengthLimit = 2;
    private const int MaxWordLengthLimit = 8;
    
    private ISettingsService SettingsService { get; }
    private SupportedLanguage preferredLanguage;
    private SupportedTheme preferredTheme;
    private int minWordLength;
    private int maxWordLength;
    private int boardSize;

    public ObservableCollection<SupportedLanguage> AvailableLanguages { get; } =
        new(Enum.GetValues(typeof(SupportedLanguage)).Cast<SupportedLanguage>());

    public ObservableCollection<SupportedTheme> AvailableThemes { get; } =
    new(Enum.GetValues(typeof(SupportedTheme)).Cast<SupportedTheme>());

    public SettingsViewModel(ISettingsService settingsService)
    {
        SettingsService = settingsService;
        LoadSettings();
    }

    private void LoadSettings()
    {
        PreferredLanguage = SettingsService.GetCurrentLanguage();
        PreferredTheme = SettingsService.GetCurrentTheme();

        MinWordLength = SettingsService.GetMinWordLength();
        MaxWordLength = SettingsService.GetMaxWordLength();

        BoardSize = SettingsService.GetBoardSize();
    }
    
    public SupportedLanguage PreferredLanguage
    {
        get => preferredLanguage;
        set
        {
            if (SetProperty(ref preferredLanguage, value))
            {
                SettingsService.SetCurrentLanguage(value);
            }
        }
    }

    public SupportedTheme PreferredTheme
    {
        get => preferredTheme;
        set
        {
            if (SetProperty(ref preferredTheme, value))
            {
                SettingsService.SetCurrentTheme(value);
            }
        }
    }

    public int MinWordLength
    {
        get => minWordLength;
        set
        {
            var newValue = Math.Max(MinWordLengthLimit, value);
            if (SetProperty(ref minWordLength, newValue))
            {
                SettingsService.SetMinWordLength(newValue);
            }
        }
    }

    public int MaxWordLength
    {
        get => maxWordLength;
        set
        {
            var newValue = Math.Min(MaxWordLengthLimit, value);
            if (SetProperty(ref maxWordLength, newValue))
            {
                SettingsService.SetMaxWordLength(newValue);
            }
        }
    }

    public int BoardSize
    {
        get => boardSize;
        set
        {
            if (SetProperty(ref boardSize, value))
            {
                SettingsService.SetBoardSize(value);
            }
        }
    }
}