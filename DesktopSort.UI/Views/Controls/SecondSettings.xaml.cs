﻿using System;
using System.ComponentModel;
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
        public SecondSettings()
        {
        }

        public SecondSettings(ModelCollection modelCollection)
        {
            InitializeComponent();
            Model2 = modelCollection.SettingsModel;
            modelCollection.PropertyChanged += ModelCollection_PropertyChanged;
            UpdateCheckBox();
            UpdateTheme();
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
                Console.WriteLine(model.IsDefaultSettings);
                Model2 = model.SettingsModel.Load<SettingsModel>("data.xml");
                UpdateCheckBox();
                UpdateTheme();
            }
        }

        private void UpdateTheme()
        {
            BoxTheme.SelectedItem = Model2.Advanced.AdvancedConfig.Theme;
        }

        private void UpdateCheckBox()
        {
            CheckIsUpdate.IsChecked = Model2.Advanced.AdvancedConfig.Update;
            CheckIsBackground.IsChecked = Model2.Advanced.AdvancedConfig.IsBackground;
            CheckIsDeleteDefaultDirectory.IsChecked = Model2.Advanced.AdvancedConfig.DeleteDefaultDirectory;
        }
    }
}