﻿using System;
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
using MessageBox = System.Windows.MessageBox;
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
            //По умолчанию Home
            OnPageButtonCommandExecuted("home");
            // Применение title
            SetTitle();
            SetMessage("Добро пожаловать! Версия: " + model1.GetVersion(false));

            await Task.Delay(2000);

            if (model2.Advanced.AdvancedConfig.IsBackground) PathImageBackground = model2.Advanced.AdvancedConfig.Background;

            SetMessage(!model2.Advanced.AdvancedConfig.Update
                ? "Внимание, проверка на обновления приложения была выключена в настройках"
                : "Настройки для обновления приложения были применены");


            
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

        //private string _ColorPrimary = "#FF2E1795";
        //private string _ColorSecondary = "#FF150851";

        private string _TextBoxPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private string _TextBoxPath1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

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

            model.SetInput(TextBoxPath);
            model.SetOutput(TextBoxPath1);
            //Task.Run(() => model.SearchFiles(Files, FileManager.FileMode.Copy));
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
            model.SetInput(TextBoxPath);
            model.SetOutput(TextBoxPath1);
            //Task.Run(() => model.SearchFiles(Files, FileManager.FileMode.Ignore));
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