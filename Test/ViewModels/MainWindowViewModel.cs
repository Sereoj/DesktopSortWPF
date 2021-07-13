using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Test.Infrastucture.Commands;
using Test.Models;
using Test.ViewModels.Base;
using Test.Views.Controls;

namespace Test.ViewModels
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
        private bool CanMinimalizeApplicationCommanddExecute(object p) => true;
        private void OnMinimalizeApplicationCommandExecuted(object p) => MainWindowState = WindowState.Minimized;

        #endregion

        #region CloseApplicationCommand
        private ICommand _CloseApplicationCommand;
        public ICommand CloseApplicationCommand => _CloseApplicationCommand ??= ( _CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute) );
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p) => Application.Current.Shutdown();
        #endregion

        #region PageButtonCommand
        private ICommand _PageButtonCommand;
        public ICommand PageButtonCommand => _PageButtonCommand ??= ( _PageButtonCommand = new RelayCommand(OnPageButtonCommandExecuted, CanPageButtonCommandExecute) );
        private bool CanPageButtonCommandExecute(object p) => true;
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
            MessengerVM.Messager = message;
        }

        public void SetTitle(bool isDev) => Title = isDev ? "DS Develop" : "Desktop Sort";

        private async Task Init()
        {
            var setting = ModelCollection.SettingsModel.Advanced.AdvancedConfig;
            
            ModelCollection.ThemeModel.SetTheme(setting.Theme);
            
            if (!setting.IsBackground) PathImageBackground = ImagerVM.Set(setting.Background);
            // Применение title
            SetTitle(setting.Mode == ApplicationNavigationMode.Dev);

            SetMessage("Добро пожаловать! Версия: " + Version.Get(true));

            await Task.Delay(2000);

            if (!setting.Update)
                SetMessage("Внимание, проверка на обновления приложения была выключена в настройках");
            else
            {
                SetMessage("Настройки для обновления приложения были применены");
                ListVM.UpdaterVM.GetVersion();
                ListVM.UpdaterVM.GetInfo();
                await Task.Delay(2000);
                if ( ListVM.UpdaterVM.IsUpdate() )
                {
                    SetMessage("Требуется обновление! Релиз: " + ListVM.UpdaterVM.Version);
                    ListVM.SettingsWindowViewModel.VisibilityUpdate = Visibility.Visible;
                }
                else
                {
                    SetMessage("Вы используете актуальную версию!");
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
            ListVM = new ViewModelCollection();
            ModelCollection = new ModelCollection();

            MessengerVM = ListVM.MessengerVM;
            ImagerVM = ListVM.ImagerVM;
            FileManagerVM = ListVM.FileManagerVM;

            Version = ModelCollection.VersionModel;

            Main = new Main();
            Settings = new Settings();
            MainViewModel = new MainViewModel(ListVM, ModelCollection);
            SettingsWindowViewModel = new SettingsWindowViewModel(ListVM, ModelCollection);

            ListVM.SettingsWindowViewModel = SettingsWindowViewModel;
            ImagerVM.PropertyChanged += Imager_PropertyChanged;
            //По умолчанию Home
            OnPageButtonCommandExecuted("home");
            Task.Run(Init);
        }
    }
}