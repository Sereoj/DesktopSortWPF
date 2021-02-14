using System;
using System.Windows;

namespace Test.Services.Theme
{
    /*
    * Модуль смены темы приложения
    * 
    */
    internal static class Theme
    {
        public enum ThemeTypes
        {
            Light, Dark, Classic
        }

        public static ThemeTypes CurrentTheme { get; set; }

        private static void ChangeTheme(string theme)
        {
            ResourceDictionary dictionary = new ResourceDictionary
            {
                Source = new Uri($"/Resources/Colors/{theme}.xaml", UriKind.Relative)
            };

            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }
        public static void SetTheme(ThemeTypes theme)
        {
            string themeName = null;
            CurrentTheme = theme;
            switch (theme)
            {
                case ThemeTypes.Dark: themeName = "dark"; break;
                case ThemeTypes.Light: themeName = "light"; break;
                case ThemeTypes.Classic: themeName = "classic"; break;
            }

            try
            {
                if (!string.IsNullOrEmpty(themeName))
                    ChangeTheme(themeName);
            }
            catch { }
        }

        public static void Set(string themeName)
        {
            switch (themeName)
            {
                case "dark": SetTheme(ThemeTypes.Dark); break;
                case "light": SetTheme(ThemeTypes.Light); break;
                case "classic": SetTheme(ThemeTypes.Classic); break;
            }
        }
    }
}