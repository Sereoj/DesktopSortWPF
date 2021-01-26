using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models.Settings;
using Test.Services.Theme;
using Test.ViewModels.Base;
using static Test.Services.Theme.Theme;

namespace Test.ViewModels
{
    internal class SecondSettingViewModel : ViewModel
    {

        private ThemeTypes _itemSelected;
        private SettingsModel Settings { get; set; }


        public ObservableCollection<ThemeTypes> ThemeTypesList { get; set; }
        public ThemeTypes ItemSelected { 
            set{
                Set(ref _itemSelected, value);
                ThemeSet();
            }
            get => _itemSelected; 
        }

        public ICommand UpdateCheckBox { get; }

        private bool CanUpdateCheckBoxCommandExecute(object p)
        {
            return true;
        }

        private void OnUpdateCheckBoxCommandExecuted(object p)
        {
            var checkbox = p as CheckBox;
            switch (checkbox.Name)
            {
                case "CheckIsUpdate":
                    Settings.Advanced.AdvancedConfig.Update = (bool)checkbox.IsChecked;
                    break;
                case "CheckIsBackground":
                    Settings.Advanced.AdvancedConfig.IsBackground = (bool)checkbox.IsChecked;
                    break;
            }
            SettingsModel.Update(Settings);
        }
        private void ThemeSet()
        {
            string theme;
            switch(ItemSelected)
            {
                case ThemeTypes.Dark:
                    theme = "dark";
                    SetTheme(ThemeTypes.Dark);
                    break;
                case ThemeTypes.Light:
                    theme = "light";
                    SetTheme(ThemeTypes.Light);
                    break;
                default:
                    theme = "light";
                break;
            }
            Settings.Advanced.AdvancedConfig.Theme = theme;
            SettingsModel.Update(Settings);
        }

        public SecondSettingViewModel()
        {
            Settings = SettingsModel.Instance;

            ThemeTypesList = new ObservableCollection<ThemeTypes>() { ThemeTypes.Light, ThemeTypes.Dark};

            UpdateCheckBox = new RelayCommand(OnUpdateCheckBoxCommandExecuted, CanUpdateCheckBoxCommandExecute);
        }

    }
}
