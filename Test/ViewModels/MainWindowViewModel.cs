using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models;
using Test.ViewModels.Base;
using Test.Views.Controls;

namespace Test.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {

        #region Values
        private object _SelectedItem;

        public object SelectedItem
        {
            get => _SelectedItem;
            set => Set(ref _SelectedItem, value);
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
        public MessengerVM MessengerVM { get; set; }
        public Version Version { get; }

        public MainViewModel MainViewModel { get; set; }
        public SettingsWindowViewModel SettingsWindowViewModel { get; set; }

        #endregion

        #region Commands

        #region Buttons

        #region MinimalizeApplicationCommand

        public ICommand MinimalizeApplicationCommand { get; }

        private bool CanMinimalizeApplicationCommanddExecute(object p) => true;

        private void OnMinimalizeApplicationCommandExecuted(object p) => MainWindowState = WindowState.Minimized;

        #endregion

        #region CloseApplicationCommand

        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p) => Application.Current.Shutdown();

        #endregion


        #region PageButtonCommand

        public ICommand PageButtonCommand { get; }
        public ViewModelCollection ViewModelCollection
        {
            get;
        }

        private bool CanPageButtonCommandExecute(object p) => true;

        private void OnPageButtonCommandExecuted(object p)
        {
            switch ( p )
            {
                case "settings":
                SelectedItem = Settings;
                break;
                case "home":
                SelectedItem = Main;
                break;
            }
        }

        #endregion

        #endregion

        #endregion

        public void SetMessage(string message)
        {
            MessengerVM.Messager = message;
            Result = message;
        }

        public void SetTitle() => Title = "Desktop Sort";

        private async Task Init()
        {
            //По умолчанию Home
            OnPageButtonCommandExecuted("home");

            var setting = ModelCollection.SettingsModel.Advanced.AdvancedConfig;

            if (!setting.IsBackground)
            {
                PathImageBackground = ImagerVM.Set(setting.Background);
            }

            // Применение title
            SetTitle();
            
            SetMessage("Добро пожаловать! Версия: " + Version.Get(true));

            await Task.Delay(2000);

            if (!setting.Update)
            {
                SetMessage("Внимание, проверка на обновления приложения была выключена в настройках");
            }
            else
            {
                SetMessage("Настройки для обновления приложения были применены");
                await Task.Delay(2000);
                //if (GLUpdater.Model.IsUpdate())
                //{
                //    SetMessage("Требуется обновление! Релиз: " + GLUpdater.Model.NewVersion);
                //    OnPageButtonCommandExecuted("settings");
                //}
                //else
                //{
                //    SetMessage("Вы используете актуальную версию!");
                //}
            }
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MessageChange")
                OnPropertyChanged("Result");
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

            FileManagerVM.PropertyChanged += Model_PropertyChanged;
            ImagerVM.PropertyChanged += Imager_PropertyChanged;

            Main = new Main();
            Settings = new Settings();
            MainViewModel = new MainViewModel(ListVM, ModelCollection);
            SettingsWindowViewModel = new SettingsWindowViewModel(ListVM, ModelCollection);

            Task.Run(() => Init());

            #region Commands

            MinimalizeApplicationCommand = new RelayCommand(OnMinimalizeApplicationCommandExecuted, CanMinimalizeApplicationCommanddExecute);
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            PageButtonCommand = new RelayCommand(OnPageButtonCommandExecuted, CanPageButtonCommandExecute);

            #endregion

        }
    }
}