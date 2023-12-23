using System.ComponentModel;

namespace WordPuzzleSolver.Wpf.ViewModels;

public sealed class CellViewModel : INotifyPropertyChanged
{
    private char letter;
    private bool isHighlighted;
    private int sequence;

    public int Sequence
    {
        get => sequence;
        set
        {
            sequence = value;
            OnPropertyChanged(nameof(Sequence));
        }
    }

    public char Letter
    {
        get => letter;
        set
        {
            var upperValue = char.ToUpper(value);
            if (letter == upperValue) return;
            letter = upperValue;
            OnPropertyChanged(nameof(Letter));
            OnPropertyChanged(nameof(Text));
        }
    }

    public string Text => Letter.ToString();

    public bool IsHighlighted
    {
        get => isHighlighted;
        set
        {
            if (isHighlighted == value) return;
            isHighlighted = value;
            OnPropertyChanged(nameof(IsHighlighted));
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
