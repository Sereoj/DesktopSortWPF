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
            Light, Dark, Test
        }

        public static ThemeTypes CurrentTheme { get; set; }

        private static ResourceDictionary ThemeDictionary
        {
            get { return Application.Current.Resources.MergedDictionaries[0]; }
            set { Application.Current.Resources.MergedDictionaries[0] = value; }
        }
        private static void ChangeTheme(Uri uri)
        {
            ThemeDictionary = new ResourceDictionary() { Source = uri };
        }
        private static void SetTheme(ThemeTypes theme)
        {
            string themeName = null;
            CurrentTheme = theme;
            switch (theme)
            {
                case ThemeTypes.Dark: themeName = "Dark"; break;
                case ThemeTypes.Light: themeName = "Light"; break;
            }

            try
            {
                if (!string.IsNullOrEmpty(themeName))
                    ChangeTheme(new Uri($"/Resources/Colors/{themeName}.xaml", UriKind.Relative));
            }
            catch { }
        }

        public static void Set(string themeName)
        {
            switch (themeName)
            {
                case "dark": SetTheme(ThemeTypes.Dark); break;
                case "light": SetTheme(ThemeTypes.Light); break;
            }
        }
    }
}