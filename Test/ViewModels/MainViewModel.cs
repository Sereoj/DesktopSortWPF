using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models;
using Test.Models.FileManagerModel;
using Test.Models.Settings;
using Test.ViewModels.Base;
using WK.Libraries.BetterFolderBrowserNS;
using static Test.ViewModels.FileManagerVM;

namespace Test.ViewModels
{
    public class MainViewModel: ViewModel
    {
        private string _TextBoxPath;
        private string _TextBoxPath1;
        public ViewModelCollection ListVM
        {
            get; set;
        }
        public ModelCollection ModelCollection
        {
            get;
            private set;
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
        public SettingsModel SettingsModel { get; }



        #region FileDialogButtonCommand

        public ICommand FileDialogButtonCommand { get; }

        private bool CanFileDialogButtonCommandExecute(object p) => true;

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

        private bool CanCopyButtonCommandExecute(object p) => true;

        private void OnCopyButtonCommandExecuted(object p)
        {
            var fileManager = ListVM.FileManagerVM;
            var messager = ListVM.MessengerVM;
            
            fileManager.SetInput(TextBoxPath);
            fileManager.SetOutput(TextBoxPath1);

            foreach ( BasicConfig config in SettingsModel.Items )
            {
                if ( config.IsChecked )
                    Task.Run(( ) => fileManager.SearchFilesAsyn(config, FileMode.Copy));
            }
            fileManager.DeleteDirectory(TextBoxPath, SettingsModel.Advanced.AdvancedConfig.DeleteDefaultDirectory);
            messager.Messager = "Работа завершилась!";

        }

        #endregion

        #region CutButtonCommand

        public ICommand CutButtonCommand { get; }

        private bool CanCutButtonCommandExecute(object p) => true;

        private void OnCutButtonCommandExecuted(object p)
        {
            var fileManager = ListVM.FileManagerVM;
            var messager = ListVM.MessengerVM;

            fileManager.SetInput(TextBoxPath);
            fileManager.SetOutput(TextBoxPath1);

            foreach ( BasicConfig config in SettingsModel.Items)
            {
                if (config.IsChecked)
                    Task.Run(() => fileManager.SearchFilesAsyn(config, FileMode.Move));
            }
            fileManager.DeleteDirectory(TextBoxPath, SettingsModel.Advanced.AdvancedConfig.DeleteDefaultDirectory);
            messager.Messager = "Работа завершилась!";
        }

        #endregion

        public MainViewModel()
        {
            TextBoxPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            TextBoxPath1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            CopyButtonCommand = new RelayCommand(OnCopyButtonCommandExecuted, CanCopyButtonCommandExecute);
            CutButtonCommand = new RelayCommand(OnCutButtonCommandExecuted, CanCutButtonCommandExecute);
            FileDialogButtonCommand = new RelayCommand(OnFileDialogButtonCommandExecuted, CanFileDialogButtonCommandExecute);
        }

        public MainViewModel(ViewModelCollection listVM, ModelCollection modelCollection)
        {
            ListVM = listVM;
            ModelCollection = modelCollection;

            SettingsModel = modelCollection.SettingsModel;

            var settings = SettingsModel.Advanced.AdvancedConfig;

            var InputPath = settings.InputPath;
            var OutputPath = settings.OutputPath;

            // Костыль
            if ( !string.IsNullOrEmpty(InputPath) && !string.IsNullOrEmpty(OutputPath) )
            {
                TextBoxPath = InputPath;
                TextBoxPath1 = OutputPath;
            }
            //else if ( GLConsole.Checker() )
            //{
            //    TextBoxPath = GLConsole.Param1;
            //    TextBoxPath1 = GLConsole.Param2;
            //}
        }
    }
}
