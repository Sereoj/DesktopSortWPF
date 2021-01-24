using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models.Helpers
{
    internal static class Imager
    {
        private static string Uri { get; set; }

        public static string Change(string uri)
        {
            return SetBackground(uri);
        }

        private static string SetBackground(string uri)
        {
            switch (uri)
            {
                case "standard":
                    Uri = "/Test;component/Resources/Images/Background.bmp";
                    break;
                case "light":
                    Uri = "/Test;component/Resources/Images/light.bmp";
                    break;
                case "dark":
                    Uri = "/Test;component/Resources/Images/dark.bmp";
                    break;
                default:
                    if (uri.IsFile())
                        Uri = uri;
                    break;
            }
            return Uri;
        }


    }
}
