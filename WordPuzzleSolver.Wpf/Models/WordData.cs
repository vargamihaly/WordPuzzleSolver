using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WordPuzzleSolver.Wpf.Models;

public class WordData : ObservableObject
{
    public WordData(string word, List<ConnectedLetter> connectedLetters)
    {
        Word = word;
        ConnectedLetters = connectedLetters;
    }

    public string Word { get; }
    public List<ConnectedLetter> ConnectedLetters { get; }
}
