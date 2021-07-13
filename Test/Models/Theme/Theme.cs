using System;
using System.Windows;

namespace Test.Models.Theme
{
    /*
    * Модуль смены темы приложения
    * 
    */
    public class Theme
    {
        public enum ThemeTypes
        {
            Light, Dark, Classic,
            dark, light // for old versions
        }

        public ThemeTypes CurrentTheme { get; set; }

        private void ChangeTheme(string theme)
        {
            ResourceDictionary dictionary = new ResourceDictionary
            {
                Source = new Uri($"/Resources/Colors/{theme}.xaml", UriKind.Relative)
            };

            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }
        public void SetTheme(ThemeTypes theme)
        {
            string themeName = null;
            CurrentTheme = theme;
            switch (theme)
            {
                case ThemeTypes.Dark: themeName = "dark"; break;
                case ThemeTypes.Light: themeName = "light"; break;
                case ThemeTypes.Classic: themeName = "classic"; break;

                // Fix for old config
                case ThemeTypes.dark: themeName = "dark"; break;
                case ThemeTypes.light: themeName = "light"; break;
            }

            try
            {
                if (!string.IsNullOrEmpty(themeName))
                    ChangeTheme(themeName);
            }
            catch { }
        }

        public void Set(string themeName)
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