using System;
using System.Windows;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models;
using Test.ViewModels.Base;

namespace Test.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Values
        private string _Title = "Desktop Sort 0.1";
        private WindowState _MainWindowState;
        private string _UserControl;

        private string _PathImageBackground = "/Images/Background.bmp";

        //private string _ColorPrimary = "#FF2E1795";
        //private string _ColorSecondary = "#FF150851";

        private string _TextBoxPath = "C://Windows";
        private string _TextBoxPath1 = "C://Windows/Backup";


        public string Title { get => _Title; set => Set(ref _Title, value); }

        public WindowState MainWindowState { get => _MainWindowState; set => Set(ref _MainWindowState, value); }
        public string userControl { get => _UserControl; set => Set(ref _UserControl, value); }

        public string PathImageBackground { get => _PathImageBackground; set => Set(ref _PathImageBackground, value); }

        public string TextBoxPath { get => _TextBoxPath; set => Set(ref _TextBoxPath, value); }
        public string TextBoxPath1 { get => _TextBoxPath1; set => Set(ref _TextBoxPath1, value); }
        #endregion

        #region Commands

        #region Buttons
        #region MinimalizeApplicationCommand
        public ICommand MinimalizeApplicationCommand { get; }

        private bool CanMinimalizeApplicationCommanddExecute(object p) => true;
        private void OnMinimalizeApplicationCommandExecuted(object p)
        {
            MainWindowState = WindowState.Minimized;
        }
        #endregion
        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region CopyButtonCommand
        public ICommand CopyButtonCommand { get; }

        private bool CanCopyButtonCommandExecute(object p) => true;
        private void OnCopyButtonCommandExecuted(object p)
        {
            MessageBox.Show("Copy");
        }
        #endregion

        #region CutButtonCommand
        public ICommand CutButtonCommand { get; }

        private bool CanCutButtonCommandExecute(object p) => true;
        private void OnCutButtonCommandExecuted(object p)
        {
            MessageBox.Show("Cut");
        }
        #endregion

        #region SettingsButtonCommand
        public ICommand SettingsButtonCommand { get; }

        private bool CanSettingsButtonCommandExecute(object p) => true;
        private void OnSettingsButtonCommandExecuted(object p)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            dictionary.Source = new Uri("/Resources/Colors/dark.xaml", UriKind.Relative);
            Application.Current.Resources.Clear();

            // Динамически меняем коллекцию MergedDictionaries
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
            MessageBox.Show("Settings");
        }
        #endregion

        #region InfoButtonCommand
        public ICommand InfoButtonCommand { get; }

        private bool CanInfoButtonCommandExecute(object p) => true;
        private void OnInfoButtonCommandExecuted(object p)
        {
            MessageBox.Show("Info");
        }
        #endregion

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Commands
            MinimalizeApplicationCommand = new RelayCommand(OnMinimalizeApplicationCommandExecuted, CanMinimalizeApplicationCommanddExecute);
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            CopyButtonCommand = new RelayCommand(OnCopyButtonCommandExecuted, CanCopyButtonCommandExecute);
            CutButtonCommand = new RelayCommand(OnCutButtonCommandExecuted, CanCutButtonCommandExecute);
            SettingsButtonCommand = new RelayCommand(OnSettingsButtonCommandExecuted, CanSettingsButtonCommandExecute);
            InfoButtonCommand = new RelayCommand(OnInfoButtonCommandExecuted, CanInfoButtonCommandExecute);
            #endregion
        }
    }
}
