using System;
using System.Windows;
using System.Windows.Controls;
using Test.Models;
using Test.Models.Settings;

namespace Test.Views.Controls
{
    /// <summary>
    ///     Логика взаимодействия для SecondSettings.xaml
    /// </summary>
    public partial class SecondSettings : UserControl
    {
        public SettingsModel model2
        {
            get; set;
        }
        public SecondSettings()
        {
        }

        public SecondSettings(ModelCollection modelCollection)
        {
            InitializeComponent();
            model2 = modelCollection.SettingsModel;
            UpdateCheckBox();
            UpdateTheme();
        }

        private void UpdateTheme()
        {
            BoxTheme.SelectedItem = model2.Advanced.AdvancedConfig.Theme;
        }
        private void UpdateCheckBox()
        {
            CheckIsUpdate.IsChecked = model2.Advanced.AdvancedConfig.Update;
            CheckIsBackground.IsChecked = model2.Advanced.AdvancedConfig.IsBackground;
            CheckIsDeleteDefaultDirectory.IsChecked = model2.Advanced.AdvancedConfig.DeleteDefaultDirectory;
        }
    }
}