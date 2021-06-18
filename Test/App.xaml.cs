using System.Windows;
using Test.Models;
using Test.Models.Settings;
using Test.ViewModels;
using Test.ViewModels.Base;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Test
{
    /// <summary>
    ///     Логика взаимодействия для App.xaml
    /// </summary>
    internal partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppCenter.Start("e451f2c8-a137-4826-8dd8-41c61cee8c87",
                   typeof(Analytics), typeof(Crashes));

        }
    }
}