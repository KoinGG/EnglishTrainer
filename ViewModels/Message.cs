using Avalonia.Controls;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia;

namespace EnglishTrainer.ViewModels
{
    public class Message
    {
        public static void ShowDialogMessage(Window window, string errorText, string errorTitle = "Ошибка")
        {
            MessageBoxManager.GetMessageBoxStandardWindow($"{errorTitle}", $"{errorText}", ButtonEnum.Ok, Icon.Warning).ShowDialog(window);
        }

        public static void ShowMessage(string errorText, string errorTitle = "Ошибка")
        {
            MessageBoxManager.GetMessageBoxStandardWindow($"{errorTitle}", $"{errorText}", ButtonEnum.Ok).Show();
        }
    }
}