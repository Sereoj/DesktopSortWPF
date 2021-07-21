using System;
using System.Windows.Controls;
using DesktopSort.UI.Models;
using DesktopSort.UI.Models.Settings;

namespace DesktopSort.UI.Views.Controls
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
            modelCollection.PropertyChanged += ModelCollection_PropertyChanged;
            UpdateCheckBox();
            UpdateTheme();
        }

        private void ModelCollection_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var model = sender as ModelCollection;
            if (model.IsDefaultSettings)
            {
                Console.WriteLine(model.IsDefaultSettings);
                model2 = model.SettingsModel.Load<SettingsModel>("data.xml");
                UpdateCheckBox();
                UpdateTheme();
            }
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