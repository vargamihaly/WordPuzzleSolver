using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using WordPuzzleSolver.Wpf.Services;
using System;
using WordPuzzleSolver.Common.Core;

namespace WordPuzzleSolver.Wpf.ViewModels;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Intended to be accessed by the View layer and are therefore public by necessity")]

public class SolverViewModel : ObservableObject , ISingleton
{
    private readonly List<string> wordList = new();
    private bool isSpinnerVisible;
    private ISettingsService SettingsService { get; }

    public SolverViewModel(ISettingsService settingsService)
    {
        SettingsService = settingsService;
        RegisterMessages();
        InitializeGridCells();
        LoadWordList();
    }
    
     public ObservableCollection<WordData> FoundWordsCollection { get; } = new();
        public ObservableCollection<CellViewModel> GridCells { get; } = new();
        public ICommand FindWordsCommand => new RelayCommand(FindWordsAsync);
        public ICommand ClearBoardCommand => new RelayCommand(ClearBoard);

        public bool IsSpinnerVisible
        {
            get => isSpinnerVisible;
            set => SetProperty(ref isSpinnerVisible, value);
        }

        public int BoardSize => SettingsService.GetBoardSize();

        private void RegisterMessages()
        {
            WeakReferenceMessenger.Default.Register<LanguageChangedMessage>(this, OnLanguageChanged);
            WeakReferenceMessenger.Default.Register<BoardSizeChangedMessage>(this, OnBoardSizeChanged);
        }

        private void OnLanguageChanged(object recipient, LanguageChangedMessage msg)
        {
            LoadWordList();
            FoundWordsCollection.Clear();
        }

        private void OnBoardSizeChanged(object recipient, BoardSizeChangedMessage msg)
        {
            InitializeGridCells();
            FoundWordsCollection.Clear();
        }

        private void InitializeGridCells()
        {
            GridCells.Clear();
            var random = new Random();
            for (var i = 0; i < BoardSize * BoardSize; i++)
            {
                var randomLetter = (char)random.Next('A', 'Z' + 1);
                GridCells.Add(new CellViewModel { Letter = randomLetter });
            }
        }

        private void LoadWordList()
        {
            wordList.Clear();
            var resourceName = GetResourceNameForCurrentLanguage();
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (stream == null) return;

            using var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine()?.Trim().ToLower();
                if (string.IsNullOrEmpty(line) || !IsValidWord(line))
                    continue;

                wordList.Add(line);
            }
        }
    
        private bool IsValidWord(string word)
        {
            return word.Length <= SettingsService.GetMaxWordLength() && 
                   word.Length >= SettingsService.GetMinWordLength() && 
                   char.IsLower(word[0]);
        }

        private string GetResourceNameForCurrentLanguage()
        {
            var languageCode = SettingsService.GetCurrentLanguage().GetLanguageCode();
            return $"WordPuzzleSolver.Wpf.Resources.wordlist_{languageCode}.txt";
        }

        private void ClearBoard()
        {
            foreach (var cell in GridCells)
                cell.Letter = '\0';
        }

        private async void FindWordsAsync()
        {
            IsSpinnerVisible = true;
            try
            {
                await Task.Run(PerformSearchOperation);
            }
            finally
            {
                IsSpinnerVisible = false;
            }
        }

        private void PerformSearchOperation()
        {
            var foundWords = new List<WordData>();
            var letters = GetLettersInGrid();
            var foundWordSet = new HashSet<string>();

            for (var row = 0; row < BoardSize; row++)
            {
                for (var col = 0; col < BoardSize; col++)
                    foundWords.AddRange(FindWordsDfs(row, col, letters, new StringBuilder(), new List<ConnectedLetter>(), foundWordSet));
            }

            UpdateFoundWordsCollection(foundWords);
        }

        private void UpdateFoundWordsCollection(List<WordData> foundWords)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                FoundWordsCollection.Clear();
                foreach (var wordData in foundWords.OrderBy(w => w.Word))
                    FoundWordsCollection.Add(wordData);
            });
        }
        
    public void MouseEntered(WordData? word)
    {
        HighlightWord(word);
    }
    
    public void MouseLeft()
    {
        foreach (var cell in GridCells)
        {
            cell.IsHighlighted = false;
            cell.Sequence = 0;
        }
    }
    
    private void HighlightWord(WordData? word)
    {
        if (word == null) return;
    
        foreach (var cell in GridCells)
        {
            cell.IsHighlighted = false;
            cell.Sequence = 0;
    
        }
    
        foreach (var connectedLetter in word.ConnectedLetters)
        {
            var cell = GridCells[connectedLetter.Row * 3 + connectedLetter.Column];
            cell.IsHighlighted = true;
            cell.Sequence = connectedLetter.Sequence;
            
            GridCells[connectedLetter.Row * 3 + connectedLetter.Column].IsHighlighted = true;
        }
    }
    
    // Depth-first
    private IEnumerable<WordData> FindWordsDfs(int row, int col, IList<char> letters, StringBuilder currentWord, IList<ConnectedLetter> path, ISet<string> foundWordSet)
    {
        var foundWords = new List<WordData>();
    
        if (row < 0 || row >= BoardSize || col < 0 || col >= BoardSize || letters[row * BoardSize + col] == '\0')
        {
            return foundWords;
        }
    
        var letter = letters[row * BoardSize + col];
        currentWord.Append(letter);
        path.Add(new ConnectedLetter(row, col, path.Count+1));
    
        var currentWordStr = currentWord.ToString();
    
        if (wordList.Contains(currentWordStr) && foundWordSet.Add(currentWordStr))
        {
            foundWords.Add(new WordData(currentWordStr, new List<ConnectedLetter>(path)));
        }
    
        letters[row * BoardSize + col] = '\0';
    
        for (var dr = -1; dr <= 1; dr++)
        {
            for (var dc = -1; dc <= 1; dc++)
            {
                if (dr == 0 && dc == 0) continue;
                foundWords.AddRange(FindWordsDfs(row + dr, col + dc, letters, currentWord, path, foundWordSet));
            }
        }
    
        currentWord.Remove(currentWord.Length - 1, 1);
        path.RemoveAt(path.Count - 1);
    
        letters[row * BoardSize + col] = letter;
    
        return foundWords;
    }
    
    private char[] GetLettersInGrid()
    {
    
        var letters = new char[GridCells.Count];
    
        for (var index = 0; index < GridCells.Count; index++)
        {
            letters[index] = GridCells[index].Text.ToLower()[0];
        }
    
        return letters;
    }

}