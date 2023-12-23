using WordPuzzleSolver.Wpf.ViewModels;

namespace WordPuzzleSolver.Wpf.Views
{
    public partial class HowToGuideView
    {
        public HowToGuideView()
        {
            InitializeComponent();
            DataContext = new HowToGuideViewModel();
        }
    }
}
