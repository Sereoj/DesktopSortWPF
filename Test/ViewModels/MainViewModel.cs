﻿using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models.FileManagerModel;
using Test.Models.Settings;
using Test.Models.Console;
using Test.ViewModels.Base;
using WK.Libraries.BetterFolderBrowserNS;

namespace Test.ViewModels
{
    internal class MainViewModel: ViewModel
    {
        private string _TextBoxPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private string _TextBoxPath1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        private readonly FileManager model;
        private readonly SettingsModel model2;

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

        public MainViewModel()
        {
            CopyButtonCommand = new RelayCommand(OnCopyButtonCommandExecuted, CanCopyButtonCommandExecute);
            CutButtonCommand = new RelayCommand(OnCutButtonCommandExecuted, CanCutButtonCommandExecute);
            FileDialogButtonCommand = new RelayCommand(OnFileDialogButtonCommandExecuted, CanFileDialogButtonCommandExecute);

            model = FileManager.Model;
            model2 = SettingsModel.Instance;

            var InputPath = model2.Advanced.AdvancedConfig.InputPath;
            var OutputPath = model2.Advanced.AdvancedConfig.OutputPath;

            // Костыль
            if(!string.IsNullOrEmpty(InputPath) && !string.IsNullOrEmpty(OutputPath))
            {
                TextBoxPath = InputPath;
                TextBoxPath1 = OutputPath;
            }else if( GLConsole.Checker() )
            {
                TextBoxPath = GLConsole.Param1;
                TextBoxPath1 = GLConsole.Param2;
            }
        }


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
            betterFolder.Disposed += BetterFolder_Disposed;
            betterFolder.Dispose();
        }

        private void BetterFolder_Disposed(object sender, EventArgs e)
        {
            GC.Collect(4, GCCollectionMode.Forced, true);
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

            foreach ( BasicConfig config in model2.Items )
            {
                if ( config.IsChecked )
                    Task.Run(( ) => model.SearchFilesAsyn(config, FileManager.FileMode.Copy));
            }
            model.DeleteDirectory(TextBoxPath, model2.Advanced.AdvancedConfig.DeleteDefaultDirectory);
            model.SetMessage("Работа завершилась!");

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

            foreach ( BasicConfig config in model2.Items)
            {
                if (config.IsChecked)
                    Task.Run(() => model.SearchFilesAsyn(config, FileManager.FileMode.Move));
            }
            model.DeleteDirectory(TextBoxPath, model2.Advanced.AdvancedConfig.DeleteDefaultDirectory);
            model.SetMessage("Работа завершилась!");
        }

        #endregion
    }
}