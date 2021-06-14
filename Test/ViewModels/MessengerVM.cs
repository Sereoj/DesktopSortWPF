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
        public string Messager
        {
            set
            {
                if ( value.Length == 0 || string.IsNullOrEmpty(value) )
                {
                    return;
                }
                _message = value;
                OnPropertyChanged("MessageChange");
            }
            get
            {
                return _message;
            }
        }

        public void SetMessage(string message) => Messager = message; // alternative
    }
}
