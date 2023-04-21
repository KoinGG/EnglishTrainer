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

        #region [Constants]

        private const int FilterFromNewToOld = 0;
        private const int FilterFromOldToNew = 1;
        private const int FilterFromHighToLowPercentage = 2;
        private const int FilterFromLowToHighPercentage = 3;

        #endregion

        private ObservableCollection<ResultHistory> _resultHistories;

        private int _selectedFilter;

        #endregion

        public HistoryWindowVM()
        {
            var ResultHistoryList = DbContextProvider.GetContext().ResultHistories.Where(x => x.UserId == AuthWindowVM.CurrentUser.UserId).OrderByDescending(x => x.Date).ThenByDescending(x => x.Time).ToList();
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

        public int SelectedFilter
        {
            get 
            { 
                return _selectedFilter; 
            }
            set
            {
                if(value == FilterFromNewToOld)
                {
                    var ResultHistoryListFromOldToNew = DbContextProvider.GetContext().ResultHistories.Where(x => x.UserId == AuthWindowVM.CurrentUser.UserId).OrderByDescending(x => x.Date).ThenByDescending(x => x.Time).ToList();
                    ResultHistories = new ObservableCollection<ResultHistory>(ResultHistoryListFromOldToNew);
                }
                else if(value == FilterFromOldToNew)
                {
                    var ResultHistoryListFromNewToOld = DbContextProvider.GetContext().ResultHistories.Where(x => x.UserId == AuthWindowVM.CurrentUser.UserId).OrderBy(x => x.Date).ThenBy(x => x.Time).ToList();
                    ResultHistories = new ObservableCollection<ResultHistory>(ResultHistoryListFromNewToOld);                    
                }
                else if(value == FilterFromHighToLowPercentage)
                {
                    var ResultHistoryListFromHighToLowPercentage = DbContextProvider.GetContext().ResultHistories.Where(x => x.UserId == AuthWindowVM.CurrentUser.UserId).OrderByDescending(x => x.CurrentAnswerPersentage).ToList();
                    ResultHistories = new ObservableCollection<ResultHistory>(ResultHistoryListFromHighToLowPercentage);
                }
                else if(value == FilterFromLowToHighPercentage)
                {
                    var ResultHistoryListFromLowToHighPercentage = DbContextProvider.GetContext().ResultHistories.Where(x => x.UserId == AuthWindowVM.CurrentUser.UserId).OrderBy(x => x.CurrentAnswerPersentage).ToList();
                    ResultHistories = new ObservableCollection<ResultHistory>(ResultHistoryListFromLowToHighPercentage);
                }

                this.RaiseAndSetIfChanged(ref _selectedFilter, value);
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
