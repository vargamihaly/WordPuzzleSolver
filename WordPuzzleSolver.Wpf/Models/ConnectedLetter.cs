namespace WordPuzzleSolver.Wpf.Models;

public class ConnectedLetter
{
    public ConnectedLetter(int row, int col, int sequence)
    {
        Row = row;
        Column = col;
        Sequence = sequence;
    }

    public int Row { get; }
    public int Column { get; }
    public int Sequence { get; }
}