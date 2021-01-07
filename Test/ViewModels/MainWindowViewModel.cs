using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models;
using Test.Models.Settings;
using Test.ViewModels.Base;
using Test.Views.Controls;
using WK.Libraries.BetterFolderBrowserNS;
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
            model.PropertyChanged += Model_PropertyChanged;

            #region Commands

            MinimalizeApplicationCommand = new RelayCommand(OnMinimalizeApplicationCommandExecuted,
                CanMinimalizeApplicationCommanddExecute);
            CloseApplicationCommand =
                new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            CopyButtonCommand = new RelayCommand(OnCopyButtonCommandExecuted, CanCopyButtonCommandExecute);
            CutButtonCommand = new RelayCommand(OnCutButtonCommandExecuted, CanCutButtonCommandExecute);
            PageButtonCommand = new RelayCommand(OnPageButtonCommandExecuted, CanPageButtonCommandExecute);
            FileDialogButtonCommand =
                new RelayCommand(OnFileDialogButtonCommandExecuted, CanFileDialogButtonCommandExecute);

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
            //По умоланию Home
            OnPageButtonCommandExecuted("home");
            // Применение title
            SetTitle();
            SetMessage("Добро пожаловать! Версия: " + model1.GetVersion(false));

            if (!SettingsModel.Instance.Update)
            {
                await Task.Delay(3000);
                SetMessage("Внимание, проверка на обновления приложения была выключена в настройках");
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

        private string _PathImageBackground = "/Images/Background.bmp";

        //private string _ColorPrimary = "#FF2E1795";
        //private string _ColorSecondary = "#FF150851";

        private string _TextBoxPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private string _TextBoxPath1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        private readonly Main main = new Main();
        private readonly Settings settings = new Settings();
        private readonly FileManager model;
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

        public string TextBoxPath
        {
            get => _TextBoxPath;
            set => Set(ref _TextBoxPath, value);
        }

        public string TextBoxPath1
        {
            get => _TextBoxPath1;
            set => Set(ref _TextBoxPath1, value);
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


        #region FileDialogButtonCommand

        public ICommand FileDialogButtonCommand { get; }

        private bool CanFileDialogButtonCommandExecute(object p)
        {
            return true;
        }

        private void OnFileDialogButtonCommandExecuted(object p)
        {
            var betterFolder = new BetterFolderBrowser();
            if (betterFolder.ShowDialog() == DialogResult.OK)
                switch (p)
                {
                    case "input":
                        TextBoxPath = betterFolder.SelectedFolder;
                        break;
                    case "output":
                        TextBoxPath1 = betterFolder.SelectedFolder;
                        break;
                }

            betterFolder.Dispose();
        }

        #endregion

        #region CopyButtonCommand

        public ICommand CopyButtonCommand { get; }

        private bool CanCopyButtonCommandExecute(object p)
        {
            return true;
        }

        private void OnCopyButtonCommandExecuted(object p)
        {
            var Files = new List<Setting>
            {
                new Setting {Catalog = "Word", Extension = ".docx,.dotx,.doc,.dot"},
                new Setting {Catalog = "Excel", Extension = ".xlsx,.xlsm,.xltx,.xltm,.xlam,.xls,.xlt,.xla"},
                new Setting {Catalog = "Access", Extension = ".accdb,.mdb"},
                new Setting {Catalog = "Image", Extension = ".bmp,.tif,.jpg,.gif,.png,.ico"},
                new Setting {Catalog = "Text files", Extension = ".txt,.log"},
                new Setting {Catalog = "Project", Extension = ".mpp"},
                new Setting {Catalog = "Archive", Extension = ".rar,.zip,.7z"},
                new Setting {Catalog = "eBook", Extension = ".fb2,.epub,.mobi,.pdf,.djvu"},
                new Setting {Catalog = "Media", Extension = ".avi,.mp4,.mpeg,.wmv,.mp3"}
            };

            model.SetInput(TextBoxPath);
            model.SetOutput(TextBoxPath1);
            Task.Run(() => model.SearchFiles(Files, FileManager.FileMode.Copy));
        }

        #endregion

        #region CutButtonCommand

        public ICommand CutButtonCommand { get; }

        private bool CanCutButtonCommandExecute(object p)
        {
            return true;
        }

        private void OnCutButtonCommandExecuted(object p)
        {
            var Files = new List<Setting>
            {
                new Setting {Catalog = "Word", Extension = ".docx,.dotx,.doc,.dot"},
                new Setting {Catalog = "Excel", Extension = ".xlsx,.xlsm,.xltx,.xltm,.xlam,.xls,.xlt,.xla"},
                new Setting {Catalog = "Access", Extension = ".accdb,.mdb"},
                new Setting {Catalog = "Image", Extension = ".bmp,.tif,.jpg,.gif,.png,.ico"},
                new Setting {Catalog = "Text files", Extension = ".txt,.log"},
                new Setting {Catalog = "Project", Extension = ".mpp"},
                new Setting {Catalog = "Archive", Extension = ".rar,.zip,.7z"},
                new Setting {Catalog = "eBook", Extension = ".fb2,.epub,.mobi,.pdf,.djvu"},
                new Setting {Catalog = "Media", Extension = ".avi,.mp4,.mpeg,.wmv,.mp3"}
            };

            model.SetInput(TextBoxPath);
            model.SetOutput(TextBoxPath1);
            Task.Run(() => model.SearchFiles(Files, FileManager.FileMode.Ignore));
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
            //ResourceDictionary dictionary = new ResourceDictionary();
            //dictionary.Source = new Uri("/Resources/Colors/dark.xaml", UriKind.Relative);

            //Application.Current.Resources.Clear();
            // Динамически меняем коллекцию MergedDictionaries
            //Application.Current.Resources.MergedDictionaries.Add(dictionary);
            switch (p)
            {
                case "settings":
                    SelectedItem = settings;
                    //dictionary.Source = new Uri("Resources/Colors/dark.xaml", UriKind.Relative);
                    break;
                case "home":
                    SelectedItem = main;
                    //dictionary.Source = new Uri("Resources/Colors/light.xaml", UriKind.Relative);
                    break;
            }

            //Application.Current.Resources.Clear();
            //Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }

        #endregion

        #endregion

        #endregion
    }
}