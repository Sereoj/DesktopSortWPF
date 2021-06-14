using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models;
using Test.Models.Settings;
using Test.ViewModels.Base;
using static Test.Models.Theme.Theme;

namespace Test.ViewModels
{
    public class SecondSettingViewModel : ViewModel, IApplicationContentView
    {

        private ThemeTypes _itemSelected;
        private bool _isLoading;

        public string Name => "Настройки // Параметры приложения";

        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }
        public SettingsModel Settings { get; set; }
        public ImagerVM Imager { get; set; }

        public ObservableCollection<ThemeTypes> ThemeTypesList { get; set; }

        public ViewModelCollection ListVM { get; set; }
        public ModelCollection ModelCollection { get; set; }
        public ThemeTypes ItemSelected
        {
            set
            {
                Set(ref _itemSelected, value);
            }
            get => _itemSelected; 
        }
        private string _BackgroundChanger;
        public string BackgroundChanger
        {
            get => _BackgroundChanger;
            set
            {
                Set(ref _BackgroundChanger, value);
                //BackgroundChange();
            }
        }

        public ICommand ButtonSaveCommand { get; }

        private bool CanButtonSaveCommandExecute(object p)
        {
            return true;
        }

        private void OnButtonSaveCommandExecuted(object p)
        {
            MessageBox.Show("Save");
        }

        public ICommand UpdateCheckBox { get; }


        private bool CanUpdateCheckBoxCommandExecute(object p)
        {
            return true;
        }

        private void OnUpdateCheckBoxCommandExecuted(object p)
        {
            var checkbox = p as CheckBox;
            MessageBox.Show("Update");
        }


        public void Init()
        {
            
        }

        public SecondSettingViewModel()
        {
            ThemeTypesList = new ObservableCollection<ThemeTypes>() { ThemeTypes.Light, ThemeTypes.Dark, ThemeTypes.Classic };

            UpdateCheckBox = new RelayCommand(OnUpdateCheckBoxCommandExecuted, CanUpdateCheckBoxCommandExecute);
            ButtonSaveCommand = new RelayCommand(OnButtonSaveCommandExecuted, CanButtonSaveCommandExecute);
        }

        public SecondSettingViewModel(ViewModelCollection listVM, ModelCollection modelCollection)
        {
            ListVM = listVM;
            ModelCollection = modelCollection;
            Imager = ListVM.ImagerVM;
            ItemSelected = ThemeTypes.Dark;

            ThemeTypesList = new ObservableCollection<ThemeTypes>() { ThemeTypes.Light, ThemeTypes.Dark, ThemeTypes.Classic };

            UpdateCheckBox = new RelayCommand(OnUpdateCheckBoxCommandExecuted, CanUpdateCheckBoxCommandExecute);
            ButtonSaveCommand = new RelayCommand(OnButtonSaveCommandExecuted, CanButtonSaveCommandExecute);
        }
    }
}
