using System.Windows.Controls;
using System.Windows.Input;
using WordPuzzleSolver.Wpf.ViewModels;

namespace WordPuzzleSolver.Wpf.Views;

public partial class SolverView
{
    public SolverView()
    {
        InitializeComponent();
    }
    
    private void Button_MouseEnter(object sender, MouseEventArgs e)
    {
        if (sender is not Button { Tag: WordData wordData }) return;
        if (DataContext is SolverViewModel vm)
        {
            vm.MouseEntered(wordData);
        }
    }

    private void Button_MouseLeave(object sender, MouseEventArgs e)
    {
        if (sender is not Button) return;
        if (DataContext is SolverViewModel vm)
        {
            vm.MouseLeft();
        }
    }
}