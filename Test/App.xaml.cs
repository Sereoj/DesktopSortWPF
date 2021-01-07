using System.Windows;
using Test.Models.Settings;

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
            SettingsModel.Load<SettingsModel>("data.xml");
        }
    }
}