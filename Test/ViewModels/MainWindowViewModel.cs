using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models.FileManagerModel;
using Test.Models.Helpers;
using Test.Models.Settings;
using Test.Services.GLUpdater;
using Test.ViewModels.Base;
using Test.Views.Controls;
using Application = System.Windows.Application;
using Version = Test.Models.Version;

namespace Test.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            model = FileManager.Model;
            model1 = Version.Model;
            model2 = SettingsModel.Instance;



            model.PropertyChanged += Model_PropertyChanged;

            #region Commands

            MinimalizeApplicationCommand = new RelayCommand(OnMinimalizeApplicationCommandExecuted,CanMinimalizeApplicationCommanddExecute);
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            PageButtonCommand = new RelayCommand(OnPageButtonCommandExecuted, CanPageButtonCommandExecute);

            #endregion

            Task.Run(Init);
        }

        private void SetTitle()
        {
            Title = "Desktop Sort";
        }

        private void SetMessage(string text)
        {
            model.SetMessage(text);
        }

        private async Task Init()
        {
            //По умолчанию Home
            OnPageButtonCommandExecuted("home");

            if (!model2.Advanced.AdvancedConfig.IsBackground)
            {
                PathImageBackground = Imager.Change(model2.Advanced.AdvancedConfig.Background);
            }

            // Применение title
            SetTitle();
            SetMessage("Добро пожаловать! Версия: " + model1.GetVersion(false));

            await Task.Delay(2000);

            if (!model2.Advanced.AdvancedConfig.Update)
            {
                SetMessage("Внимание, проверка на обновления приложения была выключена в настройках");
            }
            else
            {
                SetMessage("Настройки для обновления приложения были применены");
                await Task.Delay(2000);
                if (GLUpdater.Model.IsUpdate())
                {
                    SetMessage("Требуется обновление! Релиз: " + GLUpdater.Model.NewVersion);
                    OnPageButtonCommandExecuted("settings");
                }
                else
                {
                    SetMessage("Вы используете актуальную версию!");
                }
            }



        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MessageChange")
                OnPropertyChanged("Result");
        }

        #region Values

        private string _Title;
        private WindowState _MainWindowState;

        private string _PathImageBackground;



        private readonly Main main = new Main();
        private readonly Settings settings = new Settings();
        private readonly FileManager model;
        private readonly SettingsModel model2;
        private readonly Version model1;
        private object _SelectedItem;

        public object SelectedItem
        {
            get => _SelectedItem;
            set => Set(ref _SelectedItem, value);
        }

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        public WindowState MainWindowState
        {
            get => _MainWindowState;
            set => Set(ref _MainWindowState, value);
        }

        public string PathImageBackground
        {
            get => _PathImageBackground;
            set => Set(ref _PathImageBackground, value);
        }


        public string Result => model.GetMessage;

        #endregion

        #region Commands

        #region Buttons

        #region MinimalizeApplicationCommand

        public ICommand MinimalizeApplicationCommand { get; }

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

        public ICommand CloseApplicationCommand { get; }

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

        public ICommand PageButtonCommand { get; }

        private bool CanPageButtonCommandExecute(object p)
        {
            return true;
        }

        private void OnPageButtonCommandExecuted(object p)
        {
            switch (p)
            {
                case "settings":
                    SelectedItem = settings;
                    break;
                case "home":
                    SelectedItem = main;
                    break;
            }
        }

        #endregion

        #endregion

        #endregion
    }
}