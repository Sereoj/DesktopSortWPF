using System;
using System.Globalization;
using System.Windows;

namespace Test
{
    /// <summary>
    ///     Логика взаимодействия для App.xaml
    /// </summary>
    internal partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(CultureInfo.InstalledUICulture.Name);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
        }
    }
}