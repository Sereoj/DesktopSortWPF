using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DesktopSort.UI.Infrastucture.Commands;
using DesktopSort.UI.Models;
using DesktopSort.UI.ViewModels.Base;
using DesktopSort.UI.Views.Controls;
using Version = DesktopSort.UI.Models.Version;

namespace DesktopSort.UI.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {

        #region Values
        private UserControl _SelectedView;
        public UserControl SelectedView
        {
            get => _SelectedView;
            set => Set(ref _SelectedView, value);
        }
        private Visibility _VisibilityImageBackground;
        public Visibility VisibilityImageBackground
        {
            get => _VisibilityImageBackground;
            set => Set(ref _VisibilityImageBackground, value);
        }
        private string _Title;
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        private WindowState _MainWindowState;
        public WindowState MainWindowState
        {
            get => _MainWindowState;
            set => Set(ref _MainWindowState, value);
        }
        private string _PathImageBackground;
        public string PathImageBackground
        {
            get => _PathImageBackground;
            set => Set(ref _PathImageBackground, value);
        }

        private string _Result;
        public string Result { get => _Result; set => Set(ref _Result, value); }

        public ViewModelCollection ListVM
        {
            get; set;
        }
        public string NameOfResourceKey
        {
            get; set;
        }
        public ModelCollection ModelCollection { get; set; }
        public Settings Settings { get; set; }
        public Main Main { get; set; }
        public ImagerVM ImagerVM { get; set; }
        public FileManagerVM FileManagerVM { get; set; }
        public MessengerVM MessengerVM {
            get; set;
        }
        public Version Version { get; }
        public MainViewModel MainViewModel { get; set; }
        public SettingsWindowViewModel SettingsWindowViewModel { get; set; }
        #endregion

        #region Commands

        #region Buttons

        #region MinimalizeApplicationCommand
        private ICommand _MinimalizeApplicationCommand;
        public ICommand MinimalizeApplicationCommand => _MinimalizeApplicationCommand ??= ( _MinimalizeApplicationCommand = new RelayCommand(OnMinimalizeApplicationCommandExecuted, CanMinimalizeApplicationCommanddExecute) );
        private bool CanMinimalizeApplicationCommanddExecute(object p)
        {
            return true;
        }

        private void OnMinimalizeApplicationCommandExecuted(object p)
        {
            MainWindowState = WindowState.Minimized;
        }

        #endregion

        #region CloseApplicationCommand
        private ICommand _CloseApplicationCommand;
        public ICommand CloseApplicationCommand => _CloseApplicationCommand ??= ( _CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute) );
        private bool CanCloseApplicationCommandExecute(object p)
        {
            return true;
        }

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region PageButtonCommand
        private ICommand _PageButtonCommand;
        public ICommand PageButtonCommand => _PageButtonCommand ??= ( _PageButtonCommand = new RelayCommand(OnPageButtonCommandExecuted, CanPageButtonCommandExecute) );
        private bool CanPageButtonCommandExecute(object p)
        {
            return true;
        }

        private void OnPageButtonCommandExecuted(object p)
        {
            switch ( p )
            {
                case "settings":
                    SelectedView = Settings;
                    SelectedView.DataContext = SettingsWindowViewModel;
                break;
                case "home":
                    SelectedView = Main;
                    SelectedView.DataContext = MainViewModel;
                break;
            }
        }
        #endregion
        #endregion
        #endregion

        public void SetMessage(string message)
        {
            SetMessage(null, null);
            MessengerVM.SetMessage(message);
        }

        public void SetMessage(string message,string sec)
        {
            MessengerVM.SetMessage(message, sec);
        }

        public void SetTitle(bool isDev)
        {
            Title = isDev ? "DS Develop" : "Desktop Sort";
        }

        private async Task Init()
        {
            var setting = ModelCollection.SettingsModel.Advanced.AdvancedConfig;
            
            ModelCollection.ThemeModel.SetTheme(setting.Theme);
            if (!setting.IsBackground) PathImageBackground = ImagerVM.Set(setting.Background);
            // Применение title
            SetTitle(setting.Mode == ApplicationNavigationMode.Dev);

            SetMessage("MessageWelcomeNVersion", Version.Get(false));
            await Task.Delay(2000);
            if (!setting.Update)
                SetMessage("MessageIsUpdateFalse");
            else
            {
                SetMessage("MessageUsingSettingsForUpdate");
                ListVM.UpdaterVM.GetVersion();
                ListVM.UpdaterVM.GetInfo();
                await Task.Delay(2000);
                if ( ListVM.UpdaterVM.IsUpdate() )
                {
                    SetMessage("Localization.MessageIsUpdateTrue", ListVM.UpdaterVM.Version);
                    ListVM.SettingsWindowViewModel.VisibilityUpdate = Visibility.Visible;
                }
                else
                {
                    SetMessage("MessageNoUpdates");
                }
            }
        }
        private void Imager_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "BackVisible":
                    VisibilityImageBackground = Visibility.Visible;
                    break;
                case "BackHidden":
                    VisibilityImageBackground = Visibility.Hidden;
                    break;
                case "Uri":
                    PathImageBackground = ListVM.ImagerVM.Uri;
                    break;
            }
        }

        public MainWindowViewModel()
        {
            Main = new Main();
            Settings = new Settings();

            OnPageButtonCommandExecuted("home");
        }

        public MainWindowViewModel(ViewModelCollection listVM, ModelCollection modelCollection)
        {
            ListVM = listVM;
            ModelCollection = modelCollection;

            MessengerVM = ListVM.MessengerVM;
            ImagerVM = ListVM.ImagerVM;
            FileManagerVM = ListVM.FileManagerVM;

            Version = ModelCollection.VersionModel;

            Main = new Main();
            Settings = new Settings();

            MainViewModel = new MainViewModel(ListVM, ModelCollection);
            SettingsWindowViewModel = new SettingsWindowViewModel(ListVM, ModelCollection);

            ListVM.SettingsWindowViewModel = SettingsWindowViewModel;

            SettingsWindowViewModel.PropertyChanged += SettingsWindowViewModel_PropertyChanged;
            ImagerVM.PropertyChanged += Imager_PropertyChanged;

            Task.Run(Init);

            OnPageButtonCommandExecuted("home");
        }

        private void SettingsWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SettingsWindowViewModel model = sender as SettingsWindowViewModel;
            
            if (model.VisibilityDev == Visibility.Hidden)
            {
                SetTitle(false);
                MainViewModel.IgnoreFilesVisibility = Visibility.Hidden;
            }
        }
    }
}