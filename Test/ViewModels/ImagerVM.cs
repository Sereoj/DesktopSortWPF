using System.Windows;
using Test.ViewModels.Base;
using Test.Models.Helpers;

namespace Test.ViewModels
{
    public class ImagerVM : ViewModel
    {

        private string uri;
        private Visibility visible;
        public string Uri
        {
            get => uri;
            set
            {
                Set(ref uri, value);
            }
        }


        public Visibility Visible
        {
            get => visible;
            set
            {
                visible = value;
                if (visible  == Visibility.Hidden)
                    OnPropertyChanged("BackHidden");
                else
                    OnPropertyChanged("BackVisible");
            }
        }

        public string Set(string uri)
        {
            switch (uri)
            {
                case "standard":
                    Uri = "/Test;component/Resources/Images/Background.bmp";
                    OnPropertyChanged("Uri");
                    break;
                case "light":
                    Uri = "/Test;component/Resources/Images/light.bmp";
                    break;
                case "dark":
                    Uri = "/Test;component/Resources/Images/dark.bmp";
                    break;
                case "/Test;component/Images/Background.bmp":
                    Uri = "/Test;component/Resources/Images/Background.bmp";
                    OnPropertyChanged("Uri");
                    break;
                default:
                    Uri = uri;
                    break;
            }
            return Uri;
        }
    }
}
