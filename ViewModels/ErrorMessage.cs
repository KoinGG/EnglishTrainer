using Avalonia.Controls;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.ViewModels
{
    public class ErrorMessage
    {
        public static void ShowErrorMessage(Window window, string errorText, string errorTitle = "Ошибка")
        {
            MessageBoxManager.GetMessageBoxStandardWindow($"{errorTitle}", $"{errorText}", ButtonEnum.Ok, Icon.Warning).ShowDialog(window);
        }
    }
}
