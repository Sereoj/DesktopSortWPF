using System.Windows.Controls;
using Test.Models.Settings;

namespace Test.Views.Controls
{
    /// <summary>
    ///     Логика взаимодействия для FirstSettings.xaml
    /// </summary>
    public partial class FirstSettings : UserControl
    {
        private readonly SettingsModel model2;
        public FirstSettings()
        {
            InitializeComponent();
            model2 = SettingsModel.Instance;
            UpdateCheckBox();

        }


        void UpdateCheckBox()
        {
            CheckFormat1.IsChecked = model2.Items[0].IsChecked;
            CheckFormat2.IsChecked = model2.Items[1].IsChecked;
            CheckFormat3.IsChecked = model2.Items[2].IsChecked;
            CheckFormat4.IsChecked = model2.Items[3].IsChecked;
            CheckFormat5.IsChecked = model2.Items[4].IsChecked;
            CheckFormat6.IsChecked = model2.Items[5].IsChecked;
            CheckFormat7.IsChecked = model2.Items[6].IsChecked;
            CheckFormat8.IsChecked = model2.Items[7].IsChecked;
            CheckFormat9.IsChecked = model2.Items[8].IsChecked;
            CheckFormat10.IsChecked = model2.Items[9].IsChecked;
            CheckFormat11.IsChecked = model2.Items[10].IsChecked;
            CheckFormat12.IsChecked = model2.Items[11].IsChecked;
            CheckFormat13.IsChecked = model2.Items[12].IsChecked;
            CheckFormat14.IsChecked = model2.Items[13].IsChecked;
            CheckFormat15.IsChecked = model2.Items[14].IsChecked;
            CheckFormat16.IsChecked = model2.Items[15].IsChecked;
            CheckFormat17.IsChecked = model2.Items[16].IsChecked;
            CheckFormat18.IsChecked = model2.Items[17].IsChecked;
            CheckFormat19.IsChecked = model2.Items[18].IsChecked;
            CheckFormat20.IsChecked = model2.Items[19].IsChecked;
            CheckFormat21.IsChecked = model2.Items[20].IsChecked;
        }
    }
}