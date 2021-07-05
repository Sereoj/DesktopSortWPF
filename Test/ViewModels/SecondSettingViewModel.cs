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
                ThemeSet(value);
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
                BackgroundChange();
            }
        }

        private ICommand _ButtonSaveCommand;
        public ICommand ButtonSaveCommand => _ButtonSaveCommand ?? ( _ButtonSaveCommand = new RelayCommand(OnButtonSaveCommandExecuted, CanButtonSaveCommandExecute) );
        private bool CanButtonSaveCommandExecute(object p) => true;

        private void OnButtonSaveCommandExecuted(object p)
        {
            Settings.Update(Settings);
        }

        private ICommand _UpdateCheckBoxCommand;
        public ICommand UpdateCheckBoxCommand => _UpdateCheckBoxCommand ?? ( _UpdateCheckBoxCommand = new RelayCommand(OnUpdateCheckBoxCommandExecuted, CanUpdateCheckBoxCommandExecute) );

        private bool CanUpdateCheckBoxCommandExecute(object p) => true;

        private void OnUpdateCheckBoxCommandExecuted(object p)
        {
            var checkbox = p as CheckBox;
            if ( checkbox != null )
            {
                switch ( checkbox.Name )
                {
                    case "CheckIsUpdate":
                        Settings.Advanced.AdvancedConfig.Update = ( bool )checkbox.IsChecked;
                    break;
                    case "CheckIsBackground":
                        Settings.Advanced.AdvancedConfig.IsBackground = ( bool )checkbox.IsChecked;
                        if ( !( bool )checkbox.IsChecked )
                        {
                            Imager.Visible = Visibility.Visible;
                            Imager.Set(Settings.Advanced.AdvancedConfig.Background);
                        }
                        else
                            Imager.Visible = Visibility.Hidden;
                    break;
                    case "CheckIsDeleteDefaultDirectory":
                        Settings.Advanced.AdvancedConfig.DeleteDefaultDirectory = ( bool )checkbox.IsChecked;
                    break;
                }
            }
            Settings.Update(Settings);
        }

        private ICommand _FileDialogButtonCommand;
        public ICommand FileDialogButtonCommand => _FileDialogButtonCommand ?? ( _FileDialogButtonCommand = new RelayCommand(OnFileDialogButtonCommandExecuted, CanFileDialogButtonCommandExecute) );

        private bool CanFileDialogButtonCommandExecute(object p) => true;

        private void OnFileDialogButtonCommandExecuted(object p)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"
            };

            if ( dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                BackgroundChanger = dialog.FileName;
            }
            dialog.Dispose();
        }

        private void BackgroundChange()
        {
            Imager.Set(BackgroundChanger);
            Settings.Advanced.AdvancedConfig.Background = BackgroundChanger;
        }
        private void ThemeSet(ThemeTypes ItemSelected)
        {
            var ThemeModel = ModelCollection.ThemeModel;
            switch ( ItemSelected )
            {
                case ThemeTypes.Dark:
                ThemeModel.SetTheme(ThemeTypes.Dark);
                break;
                case ThemeTypes.Light:
                ThemeModel.SetTheme(ThemeTypes.Light);
                break;
                case ThemeTypes.Classic:
                ThemeModel.SetTheme(ThemeTypes.Classic);
                break;
                default:
                ThemeModel.SetTheme(ThemeTypes.Light);
                break;
            }
            Settings.Advanced.AdvancedConfig.Theme = ItemSelected;
            Settings.Update(Settings);
        }
        public void Init()
        {
            ThemeTypesList = new ObservableCollection<ThemeTypes>() { ThemeTypes.Light, ThemeTypes.Dark };

            if ( Settings.Advanced.AdvancedConfig.Mode == ApplicationNavigationMode.Dev )
            {
                ThemeTypesList.Add(ThemeTypes.Classic);
            }
        }
        public SecondSettingViewModel()
        {
            ThemeTypesList = new ObservableCollection<ThemeTypes>() { ThemeTypes.Light, ThemeTypes.Dark, ThemeTypes.Classic };
        }

        public SecondSettingViewModel(ViewModelCollection listVM, ModelCollection modelCollection)
        {
            ListVM = listVM;
            ModelCollection = modelCollection;

            Settings = ModelCollection.SettingsModel;
            Imager = ListVM.ImagerVM;
            ItemSelected = Settings.Advanced.AdvancedConfig.Theme;
            BackgroundChanger = Settings.Advanced.AdvancedConfig.Background;

            Init();

        }
    }
}
