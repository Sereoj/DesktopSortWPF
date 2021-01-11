using Test.ViewModels.Base;

namespace Test.Services.Message
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
            if (message.Length > 0)
            {
                GetMessage = message;
                OnPropertyChanged("MessageChange");
            }
            else
            {
                GetMessage = "...";
                OnPropertyChanged("MessageChange");
            }
        }
    }
}