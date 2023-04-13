using Avalonia.Controls;
using EnglishTrainer.ViewModels;

namespace EnglishTrainer.Views
{
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            DataContext = new MenuWindowVM();
            InitializeComponent();
        }
    }
}
