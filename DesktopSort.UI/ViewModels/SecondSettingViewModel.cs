using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using DesktopSort.UI.Infrastucture.Commands;
using DesktopSort.UI.Models;
using DesktopSort.UI.Models.FileManagerModel;
using DesktopSort.UI.Models.Settings;
using DesktopSort.UI.ViewModels.Base;
using static DesktopSort.UI.Models.Theme.Theme;
using CheckBox = System.Windows.Controls.CheckBox;

namespace DesktopSort.UI.ViewModels
{
    public class SecondSettingViewModel : ViewModel, IApplicationContentView
    {
        private string _BackgroundChanger;

        private ICommand _ButtonSaveCommand;

        private ICommand _FileDialogButtonCommand;
        private bool _isLoading;
        private Locale.Translation _langSelected;
        private ThemeTypes _themeSelected;

        private ICommand _UpdateCheckBoxCommand;

        public SecondSettingViewModel()
        {
            ThemeTypesList = new ObservableCollection<ThemeTypes>
                {ThemeTypes.Light, ThemeTypes.Dark, ThemeTypes.Classic};
            LangCollection = new ObservableCollection<Locale.Translation>
                {Locale.Translation.Russia, Locale.Translation.English};
        }

        public SecondSettingViewModel(ViewModelCollection listVm, ModelCollection modelCollection)
        {
            ListVM = listVm;
            ModelCollection = modelCollection;

            Settings = ModelCollection.SettingsModel;
            Imager = ListVM.ImagerVM;
            ThemeSelected = Settings.Advanced.AdvancedConfig.Theme;
            LangSelected = Settings.Advanced.AdvancedConfig.Language;
            BackgroundChanger = Settings.Advanced.AdvancedConfig.Background;

            Init();
        }

        public SettingsModel Settings
        {
            get;
            set;
        }

        public ImagerVM Imager
        {
            get;
            set;
        }

        public ObservableCollection<ThemeTypes> ThemeTypesList
        {
            get;
            set;
        }

        public ObservableCollection<Locale.Translation> LangCollection
        {
            get;
            set;
        }

        public ViewModelCollection ListVM
        {
            get;
            set;
        }

        public ModelCollection ModelCollection
        {
            get;
            set;
        }

        public ThemeTypes ThemeSelected
        {
            set
            {
                Set(ref _themeSelected, value);
                ThemeSet(value);
            }
            get => _themeSelected;
        }

        public Locale.Translation LangSelected
        {
            set
            {
                Set(ref _langSelected, value);
                SetLang(value);
            }
            get => _langSelected;
        }

        public string BackgroundChanger
        {
            get => _BackgroundChanger;
            set
            {
                Set(ref _BackgroundChanger, value);
                BackgroundChange();
            }
        }

        public ICommand ButtonSaveCommand => _ButtonSaveCommand ??= _ButtonSaveCommand =
            new RelayCommand(OnButtonSaveCommandExecuted, CanButtonSaveCommandExecute);

        public ICommand UpdateCheckBoxCommand => _UpdateCheckBoxCommand ??=
            new RelayCommand(OnUpdateCheckBoxCommandExecuted, CanUpdateCheckBoxCommandExecute);

        public ICommand FileDialogButtonCommand => _FileDialogButtonCommand ??= _FileDialogButtonCommand =
            new RelayCommand(OnFileDialogButtonCommandExecuted, CanFileDialogButtonCommandExecute);

        public string Name => "SecondSettingsTitle";

        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }

        public void Init()
        {
            ThemeTypesList = new ObservableCollection<ThemeTypes>
                {ThemeTypes.Light, ThemeTypes.Dark, ThemeTypes.Classic};

            LangCollection = new ObservableCollection<Locale.Translation>();
            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "ru/DesktopSort.UI.resources.dll")) &&
                File.Exists(Path.Combine(Environment.CurrentDirectory, "en/DesktopSort.UI.resources.dll")))
            {
                LangCollection.Add(Locale.Translation.English);
                LangCollection.Add(Locale.Translation.Russia);
            }
            else
            {
                LangCollection.Add(Locale.Translation.NotFound);
            }
        }

        private bool CanButtonSaveCommandExecute(object p)
        {
            return true;
        }

        private void OnButtonSaveCommandExecuted(object p)
        {
            Settings.Update(Settings);
        }

        private bool CanUpdateCheckBoxCommandExecute(object p)
        {
            return true;
        }

        private void OnUpdateCheckBoxCommandExecuted(object p)
        {
            if (p is CheckBox checkbox)
                switch (checkbox.Name)
                {
                    case "CheckIsUpdate":
                        Settings.Advanced.AdvancedConfig.Update = (bool) checkbox.IsChecked;
                        break;
                    case "CheckIsBackground":
                        Settings.Advanced.AdvancedConfig.IsBackground = (bool) checkbox.IsChecked;
                        if (!(bool) checkbox.IsChecked)
                        {
                            Imager.Visible = Visibility.Visible;
                            Imager.Set(Settings.Advanced.AdvancedConfig.Background);
                        }
                        else
                            Imager.Visible = Visibility.Hidden;

                        break;
                    case "CheckIsDeleteDefaultDirectory":
                        Settings.Advanced.AdvancedConfig.DeleteDefaultDirectory = (bool) checkbox.IsChecked;
                        break;
                }

            Settings.Update(Settings);
        }

        private bool CanFileDialogButtonCommandExecute(object p)
        {
            return true;
        }

        private void OnFileDialogButtonCommandExecuted(object p)
        {
            var dialog = new OpenFileDialog
            {
                Filter = @"Image files (*.jpg, *.jpeg, *.png, *.gif) | *.jpg; *.jpeg; *.png; *.gif"
            };

            if (dialog.ShowDialog() == DialogResult.OK) BackgroundChanger = dialog.FileName;
            dialog.Dispose();
        }

        private void BackgroundChange()
        {
            Imager.Set(BackgroundChanger);
            Settings.Advanced.AdvancedConfig.Background = BackgroundChanger;
        }

        private void ThemeSet(ThemeTypes itemSelected)
        {
            var themeModel = ModelCollection.ThemeModel;
            switch (itemSelected)
            {
                case ThemeTypes.Dark:
                    themeModel.SetTheme(ThemeTypes.Dark);
                    break;
                case ThemeTypes.Light:
                    themeModel.SetTheme(ThemeTypes.Light);
                    break;
                case ThemeTypes.Classic:
                    themeModel.SetTheme(ThemeTypes.Classic);
                    break;
                default:
                    themeModel.SetTheme(ThemeTypes.Light);
                    break;
            }

            Settings.Advanced.AdvancedConfig.Theme = itemSelected;
            Settings.Update(Settings);
        }

        private void SetLang(Locale.Translation itemTranslation)
        {
            Locale.Set(itemTranslation);
            Settings.Advanced.AdvancedConfig.Language = itemTranslation;
            Settings.Update(Settings);
        }
    }
}