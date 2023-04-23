using Avalonia;
using Avalonia.Controls;
using EnglishTrainer.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.ViewModels
{
    public class MenuWindowVM : ViewModelBase
    {
        #region [Private Fields]



        #endregion

        public MenuWindowVM()
        {
            StartCommand = ReactiveCommand.Create<Window>(StartCommandImpl);
            HistoryCommand = ReactiveCommand.Create<Window>(HistoryCommandImpl);
            AddWordsCommand = ReactiveCommand.Create<Window>(AddWordsCommandImpl);
            QuitCommand = ReactiveCommand.Create<Window>(QuitCommandImpl);
        }

        #region [Properties]



        #region [Commands Declaration]

        public ReactiveCommand<Window, Unit> StartCommand { get; }
        public ReactiveCommand<Window, Unit> HistoryCommand { get; }
        public ReactiveCommand<Window, Unit> AddWordsCommand { get; }
        public ReactiveCommand<Window, Unit> QuitCommand { get; }

        #endregion

        #endregion

        #region [Methods]

        private async void StartCommandImpl(Window window)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            window.Close();
        }

        private async void HistoryCommandImpl(Window window)
        {
            var historyWindow = new HistoryWindow();
            historyWindow.Show();
            window.Close();
        }

        private async void AddWordsCommandImpl(Window window)
        {
            var addWordsWindow = new AddWordsWindow();
            addWordsWindow.Show();
            window.Close();
        }

        private void QuitCommandImpl(Window window)
        {
            AuthWindowVM.CurrentUser = null;

            var AuthWindow = new AuthWindow();
            AuthWindow.Show();
            window.Close();
        }

        #endregion
    }
}
