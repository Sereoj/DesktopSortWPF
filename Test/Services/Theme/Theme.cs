using System;
using System.Windows;

namespace Test.Services.Theme
{
    /*
    * Модуль смены темы приложения
    * 
    */
    internal class Theme
    {

        private static void Changer(string theme)
        {
            ResourceDictionary dictionary = new ResourceDictionary
            {
                Source = new Uri($"/Resources/Colors/{theme}.xaml", UriKind.Relative)
            };

            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }
        public static void Set(string theme)
        {
            switch (theme)
            {
                case "light":
                    Changer(theme);
                    //CurrectTheme = theme;
                    break;
                case "dark":
                    Changer(theme);
                    //CurrectTheme = theme;
                    break;
            }
        }
    }
}