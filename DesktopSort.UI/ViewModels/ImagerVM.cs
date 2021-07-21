using System.Windows;
using DesktopSort.UI.ViewModels.Base;

namespace DesktopSort.UI.ViewModels
{
    public class ImagerVM : ViewModel
    {
        private string uri;
        private Visibility visible;

        public string Uri
        {
            get => uri;
            set => Set(ref uri, value);
        }

        public Visibility Visible
        {
            get => visible;
            set
            {
                visible = value;
                if (visible == Visibility.Hidden)
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
                    Uri = "/DesktopSort.UI;component/Resources/Images/Background.bmp";
                    OnPropertyChanged("Uri");
                    break;
                case "standart":
                    Uri = "/DesktopSort.UI;component/Resources/Images/Background.bmp";
                    OnPropertyChanged("Uri");
                    break;
                case "default":
                    Uri = "/DesktopSort.UI;component/Resources/Images/Background.bmp";
                    OnPropertyChanged("Uri");
                    break;
                case "light":
                    Uri = "/DesktopSort.UI;component/Resources/Images/light.bmp";
                    break;
                case "dark":
                    Uri = "/DesktopSort.UI;component/Resources/Images/dark.bmp";
                    break;
                case "/Test;component/Images/Background.bmp":
                    Uri = "/DesktopSort.UI;component/Resources/Images/Background.bmp";
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