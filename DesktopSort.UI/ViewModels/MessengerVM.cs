using DesktopSort.UI.ViewModels.Base;

namespace DesktopSort.UI.ViewModels
{
    public class MessengerVM : ViewModel
    {
        private string _message;
        public string Messenger
        {
            set => Set(ref _message, value);
            get => _message;
        }

        private string _secondMessage;

        public string SecondMessenger
        {
            set => Set(ref _secondMessage, value);
            get => _secondMessage;
        }

        public void SetMessage(string message)
        {
            Messenger = message; // alternative
        }

        public void SetMessage(string message, string second)
        {
            Messenger = message;
            SecondMessenger = second;
        }
    }
}
