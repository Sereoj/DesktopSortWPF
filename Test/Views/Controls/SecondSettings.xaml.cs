using System;
using System.Windows.Controls;
using Test.Models.Settings;

namespace Test.Views.Controls
{
    /// <summary>
    ///     Логика взаимодействия для SecondSettings.xaml
    /// </summary>
    public partial class SecondSettings : UserControl
    {
        private readonly SettingsModel model2;

        public SecondSettings()
        {
            InitializeComponent();
            model2 = SettingsModel.Instance;
            UpdateCheckBox();
        }

        private void UpdateCheckBox()
        {
            CheckIsUpdate.IsChecked = model2.Advanced.AdvancedConfig.Update;
            CheckIsBackground.IsChecked = model2.Advanced.AdvancedConfig.IsBackground;
        }
    }
}