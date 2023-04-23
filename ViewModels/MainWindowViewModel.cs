using Avalonia.Controls;
using EnglishTrainer.Models;
using EnglishTrainer.Sourses;
using EnglishTrainer.Views;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using DynamicData;

namespace EnglishTrainer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        #region [Private Fields]

        private MainWindow _mainWindow;

        private ObservableCollection<Word> _englishWords;
        private ObservableCollection<Word> _russianWords;

        private Word _selectedEnglishWord;
        private Word _selectedRussianWord;

        private ResultHistory _resultHistory = new ResultHistory();

        private int CountAnswers;
        private int CountRightAnswers;

        #endregion

        public MainWindowViewModel(MainWindow window)
        {
            _mainWindow = window;

            Random random = new Random();
            var WordsList = DbContextProvider.GetContext().Words.Where(x => x.UserId == AuthWindowVM.CurrentUser.UserId).OrderBy(x => Guid.NewGuid()).Take(10).ToList(); // Потребовалось установить расшерение uuid-ossp для PostgreSQL

            RussianWords = new ObservableCollection<Word>(WordsList.OrderBy(x => random.Next()).ToList());
            EnglishWords = new ObservableCollection<Word>(WordsList);

            ContinueCommand = ReactiveCommand.Create<Window>(ContinueCommandImpl);
            CancelCommand = ReactiveCommand.Create<Window>(CancelCommandImpl);            
        }

        #region [Properties]

        public ObservableCollection<Word> EnglishWords
        {
            get
            {
                return _englishWords;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _englishWords, value);
            }
        }

        public ObservableCollection<Word> RussianWords
        {
            get
            {
                return _russianWords;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _russianWords, value);
            }
        }

        public ResultHistory ResultHistory
        {
            get 
            { 
                return _resultHistory; 
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _resultHistory, value);
            }
        }

        public Word SelectedEnglishWord
        {
            get
            {
                return _selectedEnglishWord;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedEnglishWord, value);
            }
        }

        public Word SelectedRussianWord
        {
            get
            {
                return _selectedRussianWord;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedRussianWord, value);

                try
                {
                    if (_selectedRussianWord != null && _selectedEnglishWord != null)
                    {
                        CountAnswers += 1;

                        if (_selectedRussianWord.WordId == _selectedEnglishWord.WordId)
                        {
                            CountRightAnswers += 1;

                            EnglishWords.Remove(_selectedEnglishWord);
                        }
                        else
                        {
                            EnglishWords.Remove(_selectedEnglishWord);
                        }

                        if(CountAnswers == 10)
                        {
                            Message.ShowMessage($"Ваш процент правильных ответов равен: {CountRightAnswers * 10}%", $"Результат");

                            _mainWindow.Btn_Continue.IsEnabled = true;

                            try
                            {
                                ResultHistory.Date = DateOnly.FromDateTime(DateTime.Now);
                                ResultHistory.Time = TimeOnly.FromDateTime(DateTime.Now);
                                ResultHistory.UserId = AuthWindowVM.CurrentUser.UserId;
                                ResultHistory.CurrentAnswerPersentage = CountRightAnswers * 10;

                                DbContextProvider.GetContext().ResultHistories.Add(ResultHistory);
                                DbContextProvider.GetContext().SaveChanges();

                                CountAnswers = 0;
                                CountRightAnswers = 0;
                            }
                            catch
                            {
                                Message.ShowMessage($"Не удалось сохранить историю", $"Ошибка");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    
                }
            }
        }

        #region [Commands Declaration]

        public ReactiveCommand<Window, Unit> ContinueCommand { get; }
        public ReactiveCommand<Window, Unit> CancelCommand { get; }     

        #endregion

        #endregion

        #region [Methods]

        private void ContinueCommandImpl(Window window)
        {
            Random random = new Random();
            var WordsList = DbContextProvider.GetContext().Words.Where(x => x.UserId == AuthWindowVM.CurrentUser.UserId).OrderBy(x => Guid.NewGuid()).Take(10).ToList();

            RussianWords.Clear();
            EnglishWords.Clear();

            RussianWords.Add(WordsList.OrderBy(x => random.Next()).ToList());
            EnglishWords.Add(WordsList);

            _mainWindow.Btn_Continue.IsEnabled = false;
        }

        private async void CancelCommandImpl(Window window)
        {
            var menuWindow = new MenuWindow();
            menuWindow.Show();
            window.Close();
        }

        #endregion
    }
}