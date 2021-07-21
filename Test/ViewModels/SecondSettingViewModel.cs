using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DesktopSort.UI.Infrastucture.Commands;
using DesktopSort.UI.Models;
using DesktopSort.UI.Models.Settings;
using DesktopSort.UI.ViewModels.Base;
using static DesktopSort.UI.Models.Theme.Theme;

namespace DesktopSort.UI.ViewModels
{
    public class SecondSettingViewModel : ViewModel, IApplicationContentView
    {
        private bool _isLoading;

        public string Name => "SecondSettingsTitle";

        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }
        public SettingsModel Settings { get; set; }
        public ImagerVM Imager { get; set; }

        public ObservableCollection<ThemeTypes> ThemeTypesList { get; set; }
        public ObservableCollection<Locale.Translation> LangCollection
        {
            get; set;
        }

        public ViewModelCollection ListVM { get; set; }
        public ModelCollection ModelCollection { get; set; }
        private ThemeTypes _themeSelected;
        public ThemeTypes ThemeSelected
        {
            set
            {
                Set(ref _themeSelected, value);
                ThemeSet(value);
            }
            get => _themeSelected; 
        }
        private Locale.Translation _langSelected;
        public Locale.Translation LangSelected
        {
            set
            {
                Set(ref _langSelected, value);
                SetLang(value);
            }
            get => _langSelected;
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
        public ICommand ButtonSaveCommand => _ButtonSaveCommand ??= ( _ButtonSaveCommand = new RelayCommand(OnButtonSaveCommandExecuted, CanButtonSaveCommandExecute) );
        private bool CanButtonSaveCommandExecute(object p) => true;

        private void OnButtonSaveCommandExecuted(object p)
        {
            Settings.Update(Settings);
        }

        private ICommand _UpdateCheckBoxCommand;
        public ICommand UpdateCheckBoxCommand => _UpdateCheckBoxCommand ??= new RelayCommand(OnUpdateCheckBoxCommandExecuted, CanUpdateCheckBoxCommandExecute);

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
        public ICommand FileDialogButtonCommand => _FileDialogButtonCommand ??= ( _FileDialogButtonCommand = new RelayCommand(OnFileDialogButtonCommandExecuted, CanFileDialogButtonCommandExecute) );

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
        private void ThemeSet(ThemeTypes itemSelected)
        {
            var themeModel = ModelCollection.ThemeModel;
            switch ( itemSelected )
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
        public void Init()
        {
            ThemeTypesList = new ObservableCollection<ThemeTypes>() { ThemeTypes.Light, ThemeTypes.Dark, ThemeTypes.Classic };
            LangCollection = new ObservableCollection<Locale.Translation>()
                {Locale.Translation.Russia, Locale.Translation.English};
        }
        public SecondSettingViewModel()
        {
            ThemeTypesList = new ObservableCollection<ThemeTypes>() { ThemeTypes.Light, ThemeTypes.Dark, ThemeTypes.Classic };
            LangCollection = new ObservableCollection<Locale.Translation>()
                {Locale.Translation.Russia, Locale.Translation.English};
        }

        public SecondSettingViewModel(ViewModelCollection listVM, ModelCollection modelCollection)
        {
            ListVM = listVM;
            ModelCollection = modelCollection;

            Settings = ModelCollection.SettingsModel;
            Imager = ListVM.ImagerVM;
            ThemeSelected = Settings.Advanced.AdvancedConfig.Theme;
            LangSelected = Settings.Advanced.AdvancedConfig.Language;
            BackgroundChanger = Settings.Advanced.AdvancedConfig.Background;

            Init();
        }
    }
}
