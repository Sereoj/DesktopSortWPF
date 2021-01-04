using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models;
using Test.ViewModels.Base;
using Test.Views.Controls;
using WK.Libraries.BetterFolderBrowserNS;

namespace Test.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Values
        private string _Title = "Desktop Sort 0.1";
        private WindowState _MainWindowState;

        private string _PathImageBackground = "/Images/Background.bmp";

        //private string _ColorPrimary = "#FF2E1795";
        //private string _ColorSecondary = "#FF150851";

        private string _TextBoxPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private string _TextBoxPath1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        private Main main = new Main();
        private Settings settings = new Settings();

        private object _SelectedItem;
        public object SelectedItem { get => _SelectedItem; set => Set(ref _SelectedItem, value); }

        public string Title { get => _Title; set => Set(ref _Title, value); }

        public WindowState MainWindowState { get => _MainWindowState; set => Set(ref _MainWindowState, value); }

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


        #region FileDialogButtonCommand
        public ICommand FileDialogButtonCommand { get; }

        private bool CanFileDialogButtonCommandExecute(object p) => true;
        private void OnFileDialogButtonCommandExecuted(object p)
        {
            BetterFolderBrowser betterFolder = new BetterFolderBrowser();
            if(betterFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                switch (p)
                {
                    case "input":
                        TextBoxPath = betterFolder.SelectedFolder;
                        break;
                    case "output":
                        TextBoxPath1 = betterFolder.SelectedFolder;
                        break;
                }
            }

        }
        #endregion

        #region CopyButtonCommand
        public ICommand CopyButtonCommand { get; }

        private bool CanCopyButtonCommandExecute(object p) => true;
        private void OnCopyButtonCommandExecuted(object p)
        {

            var Files = new List<Setting>
            {
                new Setting()
                {
                    Catalog = "Text Files",
                    Extension = "*.txt"
                },
                new Setting()
                {
                    Catalog = "Others",
                    Extension = "*.txt1, *.txt1"
                }

            };

            FileManager manager = new FileManager( TextBoxPath , TextBoxPath1);
            manager.SearchFiles(Files, FileManager.FileMode.Copy);
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

        #region PageButtonCommand
        public ICommand PageButtonCommand { get; }

        private bool CanPageButtonCommandExecute(object p) => true;
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

        public MainWindowViewModel()
        {
            //По умоланию Home
            OnPageButtonCommandExecuted("home");

            #region Commands
            MinimalizeApplicationCommand = new RelayCommand(OnMinimalizeApplicationCommandExecuted, CanMinimalizeApplicationCommanddExecute);
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            CopyButtonCommand = new RelayCommand(OnCopyButtonCommandExecuted, CanCopyButtonCommandExecute);
            CutButtonCommand = new RelayCommand(OnCutButtonCommandExecuted, CanCutButtonCommandExecute);
            PageButtonCommand = new RelayCommand(OnPageButtonCommandExecuted, CanPageButtonCommandExecute);
            FileDialogButtonCommand = new RelayCommand(OnFileDialogButtonCommandExecuted, CanFileDialogButtonCommandExecute);
            #endregion
        }
    }
}
