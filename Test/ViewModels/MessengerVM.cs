using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.ViewModels.Base;

namespace Test.ViewModels
{
    public class MessengerVM : ViewModel
    {
        private string _message;
        public string Messenger
        {
            set => Set(ref _message, value);
            get => _message;
        }

        public void SetMessage(string message) => Messenger = message; // alternative
    }
}
