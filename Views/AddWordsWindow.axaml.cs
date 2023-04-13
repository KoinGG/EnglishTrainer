using Avalonia.Controls;
using EnglishTrainer.ViewModels;

namespace EnglishTrainer.Views
{
    public partial class AddWordsWindow : Window
    {
        public AddWordsWindow()
        {
            DataContext = new AddWordsWindowVM(this);
            InitializeComponent();
        }
    }
}
