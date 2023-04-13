using Avalonia.Controls;
using EnglishTrainer.ViewModels;

namespace EnglishTrainer.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel(this);
            InitializeComponent();
        }
    }
}