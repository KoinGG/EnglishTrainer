using Avalonia.Controls;
using EnglishTrainer.ViewModels;

namespace EnglishTrainer.Views
{
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            DataContext = new AuthWindowVM();
            InitializeComponent();
        }
    }
}
