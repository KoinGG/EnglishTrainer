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
using Avalonia.Media;

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

        private int _selectedEnglishWordID;

        #endregion

        public MainWindowViewModel(MainWindow window)
        {
            _mainWindow = window;

            var WordsList = DbContextProvider.GetContext().Words.Take(10).Where(x => x.UserId == AuthWindowVM.CurrentUser.UserId).ToList();
            Random random = new Random();            

            RussianWords = new ObservableCollection<Word>(WordsList.OrderBy(x => random.Next()).ToList());
            EnglishWords = new ObservableCollection<Word>(WordsList);

            //_mainWindow.Listbox_EnglishWords.IsEnabled = false;

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

        public Word SelectedEnglishWord
        {
            get 
            {
                return _selectedEnglishWord;
            }
            set
            {
                if (value.IsAnswered == true)
                {
                    return;
                }
                
                //if(value.IsCorrect == true) 
                //{
                //    _textBlockColor = "Red";
                //}

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
                if (value.IsAnswered == true)
                {
                    return;
                }

                this.RaiseAndSetIfChanged(ref _selectedRussianWord, value);

                try
                {
                    if (_selectedRussianWord != null)
                    {
                        //MessageBoxManager.GetMessageBoxStandardWindow($"Ошибка", $"{_selectedEnglishWord.WordId}", ButtonEnum.Ok, Icon.Warning).Show();
                        if (_selectedRussianWord.WordId == _selectedEnglishWord.WordId)
                        {
                            MessageBoxManager.GetMessageBoxStandardWindow($"Ошибка", $"Правильно", ButtonEnum.Ok, Icon.Warning).Show();
                            _selectedRussianWord.IsAnswered = true;
                            _selectedEnglishWord.IsAnswered = true;

                            _selectedRussianWord.IsCorrect = true;
                            _selectedEnglishWord.IsCorrect = true;

                            EnglishWords.Remove(_selectedEnglishWord);

                            //var WordsList = DbContextProvider.GetContext().Words.Where(x => x.UserId == AuthWindowVM.CurrentUser.UserId).ToList();
                            //EnglishWords = new ObservableCollection<Word>(WordsList);
                            //_textBlockColor = Brushes.Green.ToString();

                            //this.RaiseAndSetIfChanged(ref _selectedRussianWord, value);
                        }
                        else
                        {
                            MessageBoxManager.GetMessageBoxStandardWindow($"Ошибка", $"Неправильно", ButtonEnum.Ok, Icon.Warning).Show();

                            _selectedRussianWord.IsAnswered = true;
                            _selectedEnglishWord.IsAnswered = true;
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    MessageBoxManager.GetMessageBoxStandardWindow($"Ошибка", $"Null", ButtonEnum.Ok, Icon.Warning).Show();
                }
            }
        }

        #region [Commands Declaration]

        public ReactiveCommand<Window, Unit> CancelCommand { get; }

        #endregion

        #endregion

        #region [Methods]

        private async void CancelCommandImpl(Window window)
        {
            var menuWindow = new MenuWindow();
            menuWindow.Show();
            window.Close();
        }

        //public void IfRussianWordSelectedCommand(MainWindow window)
        //{
        //    //window.Listbox_EnglishWords.SelectedItem
        //}

        #endregion
    }
}