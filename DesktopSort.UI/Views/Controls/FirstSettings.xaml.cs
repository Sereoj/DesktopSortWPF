﻿using System.ComponentModel;
using System.Windows.Controls;
using DesktopSort.UI.Models;
using DesktopSort.UI.Models.Settings;

namespace DesktopSort.UI.Views.Controls
{
    /// <summary>
    ///     Логика взаимодействия для FirstSettings.xaml
    /// </summary>
    public partial class FirstSettings : UserControl
    {
        public FirstSettings()
        {
        }

        public FirstSettings(ModelCollection modelCollection)
        {
            InitializeComponent();
            Model2 = modelCollection.SettingsModel;
            modelCollection.PropertyChanged += ModelCollection_PropertyChanged;
            UpdateCheckBox();
        }

        public SettingsModel Model2
        {
            get;
            set;
        }

        private void ModelCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var model = sender as ModelCollection;
            if (model.IsDefaultSettings)
            {
                Model2 = model.SettingsModel.Load<SettingsModel>("data.xml");
                UpdateCheckBox();
            }
        }

        private void UpdateCheckBox()
        {
            var i = 0;
            var maxCount = Model2.Items.Count - 1;
            foreach (var item in GridCheckbox.Children)
            {
                var checkbox = item as CheckBox;
                if (i <= maxCount)
                {
                    if (checkbox != null) checkbox.IsChecked = Model2.Items[i].IsChecked;
                    i++;
                }
            }
        }
    }
}