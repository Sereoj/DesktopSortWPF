using System;
using System.Collections.ObjectModel;
using System.Windows;
using Test.Models.Settings;
using Test.Services.Theme;
using Test.ViewModels.Base;
using static Test.Services.Theme.Theme;

namespace Test.ViewModels
{
    internal class SecondSettingViewModel : ViewModel
    {
        private bool _isBackgound;
        private bool _isUpdate;
        private ThemeTypes _itemSelected;
        private SettingsModel Settings { get; set; }
        public bool IsBackground { set =>Set(ref _isBackgound, value); get => _isBackgound; }
        public bool IsUpdate { set => Set(ref _isUpdate, value); get => _isUpdate; }


        public ObservableCollection<ThemeTypes> ThemeTypesList { get; set; }
        public ThemeTypes ItemSelected { 
            set{
                Set(ref _itemSelected, value);
                ThemeSet();
            }
            get => _itemSelected; 
        }

        private void ThemeSet()
        {
            switch(ItemSelected)
            {
                case ThemeTypes.Dark:
                    SetTheme(ThemeTypes.Dark);
                    break;
                case ThemeTypes.Light:
                    SetTheme(ThemeTypes.Light);
                    break;
            }
        }

        public SecondSettingViewModel()
        {
            Settings = SettingsModel.Instance;
            IsBackground = Settings.Advanced.AdvancedConfig.IsBackground;
            IsUpdate = Settings.Advanced.AdvancedConfig.Update;

            ThemeTypesList = new ObservableCollection<ThemeTypes>() { ThemeTypes.Light, ThemeTypes.Dark};
        }

    }
}
