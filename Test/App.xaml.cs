using System.Windows;
using Test.Models.Settings;
using Test.Services.Console;
using Test.Services.Theme;

namespace Test
{
    /// <summary>
    ///     Логика взаимодействия для App.xaml
    /// </summary>
    internal partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Console.Model.Controller(e);
            SettingsModel.CreateInstance();

            var settingsModel = SettingsModel.Instance;
            Theme.Set(settingsModel.Advanced.AdvancedConfig.Theme);
        }
    }
}