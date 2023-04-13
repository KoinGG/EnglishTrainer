using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using EnglishTrainer.ViewModels;
using EnglishTrainer.Views;

namespace EnglishTrainer
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new AuthWindow();
                {
                    DataContext = new AuthWindowVM();
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}