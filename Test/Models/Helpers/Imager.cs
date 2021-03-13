using System.Windows;
using Test.ViewModels.Base;

namespace Test.Models.Helpers
{
    internal class Imager : ViewModel
    {

        private static Imager _model;

        public static Imager Model => _model ??= new Imager();


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
                    if (uri.IsFile())
                    {
                        Uri = uri;
                    }
                    break;
            }
            return Uri;
        }
    }
}
