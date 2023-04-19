using Avalonia.Controls;
using EnglishTrainer.Models;
using EnglishTrainer.Sourses;
using EnglishTrainer.Views;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using ReactiveUI;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System;

namespace EnglishTrainer.ViewModels
{
    public class AuthWindowVM : ViewModelBase
    {
        #region [Private Fields]

        private User _user = new User();
        private string _login;

        #endregion

        public AuthWindowVM() 
        {
            SignInAcceptCommand = ReactiveCommand.Create<Window>(SignInAcceptCommandImpl);
        }

        #region [Properties]

        public User User
        {
            get 
            { 
                return _user; 
            }
            set 
            {
                this.RaiseAndSetIfChanged(ref _user, value);
            }
        }
        public string Login
        {
            get 
            { 
                return _login; 
            }
            set 
            { 
                this.RaiseAndSetIfChanged(ref _login, value);
            }
        }
        public static User CurrentUser { get; set; } 

        #region [Commands Declaration]

        public ReactiveCommand<Window, Unit> SignInAcceptCommand { get; }

        #endregion

        #endregion

        #region [Methods]

        private async void SignInAcceptCommandImpl(Window window)
        {
            User? user = DbContextProvider.GetContext().Users.FirstOrDefault(x => x.Login == User.Login);

            if (user != null)
            {
                CurrentUser = user;

                var menuWindow = new MenuWindow();
                menuWindow.Show();
                window.Close();
            }
            else
            {
                var result = await MessageBoxManager.GetMessageBoxStandardWindow("Ошибка", "Текст ошибки", ButtonEnum.YesNo, Icon.Question, WindowStartupLocation.CenterScreen).ShowDialog(window);

                if(result == ButtonResult.Yes) 
                {
                    try
                    {
                        DbContextProvider.GetContext().Users.Add(User);
                        DbContextProvider.GetContext().SaveChanges();
                    }
                    catch 
                    {
                        Message.ShowDialogMessage(window, "Не удалось создать пользователя");
                    }                    
                }
                else
                {
                    return;
                }
            }
        }

        #endregion
    }
}
