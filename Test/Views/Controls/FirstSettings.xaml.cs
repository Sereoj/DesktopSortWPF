using System.Windows;
using System.Windows.Controls;
using Test.Models;
using Test.Models.Settings;

namespace Test.Views.Controls
{
    /// <summary>
    ///     Логика взаимодействия для FirstSettings.xaml
    /// </summary>
    /// 
    public partial class FirstSettings : UserControl
    {
        public SettingsModel model2
        {
            get; set;
        }
        public FirstSettings()
        {
        }

        public FirstSettings(ModelCollection modelCollection)
        {
            InitializeComponent();
            model2 = modelCollection.SettingsModel;
            UpdateCheckBox();
        }

        private void UpdateCheckBox()
        {
            var i = 0;
            var maxCount = model2.Items.Count - 1;
            foreach (var item in GridCheckbox.Children)
            {
                CheckBox checkbox = item as CheckBox;
                if (i <= maxCount)
                {
                    if (checkbox != null) checkbox.IsChecked = model2.Items[i].IsChecked;
                    i++;
                }
            }
        }
    }
}