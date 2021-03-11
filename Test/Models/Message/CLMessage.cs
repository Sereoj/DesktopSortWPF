using Test.ViewModels.Base;

namespace Test.Models.Message
{

    /*
     * Модуль вывода сообщений на экран
     * 
     */

    internal class CLMessage : ViewModel
    {
        public string GetMessage { get; private set; }

        public void SetMessage(string message)
        {
            if (message.Length > 0 && !string.IsNullOrEmpty(message))
            {
                GetMessage = message;
                OnPropertyChanged("MessageChange");
            }
        }
    }
}