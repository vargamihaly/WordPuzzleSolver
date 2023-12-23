namespace WordPuzzleSolver.Wpf.Models;

public sealed class BoardSizeChangedMessage
{
    public int NewBoardSize { get; set; }

    public BoardSizeChangedMessage(int newBoardSize)
    {
        NewBoardSize = newBoardSize;
    }
}