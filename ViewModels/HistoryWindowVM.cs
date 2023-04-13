using Avalonia.Controls;
using Avalonia.Diagnostics;
using EnglishTrainer.Models;
using EnglishTrainer.Sourses;
using EnglishTrainer.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.ViewModels
{
    public class HistoryWindowVM : ViewModelBase
    {
        #region [Private Fields]

        private ObservableCollection<ResultHistory> _resultHistories;

        #endregion

        public HistoryWindowVM()
        {
            var ResultHistoryList = DbContextProvider.GetContext().ResultHistories.Where(x => x.UserId == AuthWindowVM.CurrentUser.UserId).ToList();
            ResultHistories = new ObservableCollection<ResultHistory>(ResultHistoryList);

            CancelCommand = ReactiveCommand.Create<Window>(CancelCommandImpl);
        }

        #region [Properties]

        public ObservableCollection<ResultHistory> ResultHistories
        {
            get 
            { 
                return _resultHistories; 
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _resultHistories, value);
            }

        }

        #region [Commands Declaration]

        public ReactiveCommand<Window, Unit> CancelCommand { get; }

        #endregion

        #endregion

        #region [Methods]

        private void CancelCommandImpl(Window window)
        {
            var menuWindow = new MenuWindow();
            menuWindow.Show();
            window.Close();
        }

        #endregion
    }
}
