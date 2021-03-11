using System.Windows;
using Test.Models.Settings;
using Test.Models.Console;
using Test.Models.GLUpdater;
using Test.Models.Theme;

namespace Test
{
    /// <summary>
    ///     Логика взаимодействия для App.xaml
    /// </summary>
    internal partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            GLConsole.Model.Controller(e);
            SettingsModel.CreateInstance();

            var settingsModel = SettingsModel.Instance;
            Theme.Set(settingsModel.Advanced.AdvancedConfig.Theme);
        }
    }
}