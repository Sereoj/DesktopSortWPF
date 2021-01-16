using System.Windows;
using Test.Models.Settings;
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
            var args = e.Args;
            SettingsModel.CreateInstance();

            var settingsModel = SettingsModel.Instance;
            Theme.Set(settingsModel.Advanced.AdvancedConfig.Theme);
        }
    }
}