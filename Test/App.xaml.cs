using System.Windows;
using Test.Models.Settings;
using Test.Views.Controls;

namespace Test
{
    /// <summary>
    ///     Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var args = e.Args;
            SettingsModel.CreateInstance();
        }
    }
}