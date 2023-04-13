using Avalonia.Controls;
using EnglishTrainer.ViewModels;

namespace EnglishTrainer.Views
{
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            DataContext = new HistoryWindowVM();
            InitializeComponent();
        }
    }
}
