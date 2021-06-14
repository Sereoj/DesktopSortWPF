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
            InitializeComponent();
            //model2 = SettingsModel.Instance;
            //UpdateCheckBox();

        }

        public FirstSettings(ModelCollection modelCollection)
        {
            model2 = modelCollection.SettingsModel;
            UpdateCheckBox();
        }

        private void UpdateCheckBox()
        {
            //var children = LogicalTreeHelper.GetChildren(CheckFormat1);
            //var i = 0;

            //foreach ( CheckBox item in children )
            //{
            //    i++;
            //    item.IsChecked = model2.Items[i].IsChecked;
            //}
        }
    }
}