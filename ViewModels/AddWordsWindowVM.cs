using Avalonia.Controls;
using EnglishTrainer.Models;
using EnglishTrainer.Sourses;
using EnglishTrainer.Views;
using ReactiveUI;
using System.Reactive;

namespace EnglishTrainer.ViewModels
{
    public class AddWordsWindowVM : ViewModelBase
    {

        #region [Private Fields]

        private AddWordsWindow _addWordsWindow;

        private Word _word = new Word();

        private string _englishVersion;
        private string _russianVersion;

        #endregion

        public AddWordsWindowVM(AddWordsWindow window)
        {
            _addWordsWindow = window;

            AddWordCommand = ReactiveCommand.Create<Window>(AddWordCommandImpl);
            CancelCommand = ReactiveCommand.Create<Window>(CancelCommandImpl);            
        }

        #region [Properties]

        public Word Word
        {
            get 
            { 
                return _word; 
            }
            set 
            { 
                this.RaiseAndSetIfChanged(ref _word, value);
            }
        }

        public string EnglishVersion
        {
            get 
            {
                return _englishVersion;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _englishVersion, value);
            }
        }

        public string RussianVersion
        {
            get
            {
                return _russianVersion;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _russianVersion, value);
            }
        }

        #region [Commands Declaration]

        public ReactiveCommand<Window, Unit> AddWordCommand { get; }
        public ReactiveCommand<Window, Unit> CancelCommand { get; }

        #endregion

        #endregion

        #region [Methods]

        private void AddWordCommandImpl(Window window)
        {
            try
            {
                Word.UserId = AuthWindowVM.CurrentUser.UserId;
                DbContextProvider.GetContext().Words.Add(Word);
                DbContextProvider.GetContext().SaveChanges();                

                Message.ShowDialogMessage(window, "Слово добавлено");

                _addWordsWindow.TextBlock_English.Clear();
                _addWordsWindow.TextBlock_Russian.Clear();
            }
            catch
            {
                Message.ShowDialogMessage(window, "Не удалось добавить слово");
            }
        }
        private void CancelCommandImpl(Window window)
        {
            var menuWindow = new MenuWindow();
            menuWindow.Show();
            window.Close();
        }

        #endregion
    }
}
