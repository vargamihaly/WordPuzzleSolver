using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using System;
using System.IO;
using WordPuzzleSolver.Common.Core;

namespace WordPuzzleSolver.Wpf.Services
{
    public interface ISettingsService : ISingleton
    {
        int GetMinWordLength();
        void SetMinWordLength(int length);
        int GetMaxWordLength();
        void SetMaxWordLength(int length);
        int GetBoardSize();
        void SetBoardSize(int length);
        SupportedLanguage GetCurrentLanguage();
        void SetCurrentLanguage(SupportedLanguage language);
        void SetCurrentTheme(SupportedTheme theme);
        SupportedTheme GetCurrentTheme();
    }
    
    public sealed class SettingsService : ISettingsService
    {
        private const string SettingsFilePath = "settings.json";
        private Settings Settings { get; set; }

        public SettingsService()
        {
            LoadSettings();
        }

        public int GetMinWordLength() => Settings.MinWordLength;
        public int GetMaxWordLength() => Settings.MaxWordLength;
        public int GetBoardSize() => Settings.BoardSize;
        public SupportedLanguage GetCurrentLanguage() => Settings.CurrentLanguage;
        public SupportedTheme GetCurrentTheme() => Settings.CurrentTheme;

        public void SetMinWordLength(int length)
        {
            Settings.MinWordLength = length;
            SaveSettings();
        }

        public void SetMaxWordLength(int length)
        {
            Settings.MaxWordLength = length;
            SaveSettings();
        }

        public void SetBoardSize(int length)
        {
            if (IsValidBoardSize(length))
            {
                Settings.BoardSize = length;
                NotifyBoardSizeChange(length);
                SaveSettings();
            }
        }

        public void SetCurrentLanguage(SupportedLanguage language)
        {
            Settings.CurrentLanguage = language;
            NotifyLanguageChange(language);
            SaveSettings();
        }

        public void SetCurrentTheme(SupportedTheme theme)
        {
            Settings.CurrentTheme = theme;
            NotifyThemeChange(theme);
            SaveSettings();
        }

        private void LoadSettings()
        {
            if (!File.Exists(SettingsFilePath))
            {
                CreateDefaultSettings();
            }
            else
            {
                ReadSettingsFromFile();
            }
        }

        private void CreateDefaultSettings()
        {
            Settings = new Settings
            {
                MinWordLength = 2,
                MaxWordLength = 8,
                BoardSize = 3,
                CurrentLanguage = SupportedLanguage.English,
                CurrentTheme = SupportedTheme.Windows11Dark,
            };
            SaveSettings();
        }

        private void ReadSettingsFromFile()
        {
            var json = File.ReadAllText(SettingsFilePath);
            Settings = JsonConvert.DeserializeObject<Settings>(json) 
                ?? throw new InvalidOperationException("Settings file contains invalid data.");
        }

        private void SaveSettings()
        {
            var json = JsonConvert.SerializeObject(Settings, Formatting.Indented);
            File.WriteAllText(SettingsFilePath, json);
        }

        private static bool IsValidBoardSize(int length) => length is > 2 and < 5;

        private static void NotifyBoardSizeChange(int length)
        {
            WeakReferenceMessenger.Default.Send(new BoardSizeChangedMessage(length));
        }

        private static void NotifyLanguageChange(SupportedLanguage language)
        {
            WeakReferenceMessenger.Default.Send(new LanguageChangedMessage(language));
        }

        private static void NotifyThemeChange(SupportedTheme theme)
        {
            WeakReferenceMessenger.Default.Send(new ThemeChangedMessage(theme));
        }
    }
}
