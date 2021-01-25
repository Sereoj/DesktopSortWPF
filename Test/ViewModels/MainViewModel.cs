using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models.FileManagerModel;
using Test.Models.Settings;
using Test.Services.Console;
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


            if (GLConsole.Checker())
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

            betterFolder.Dispose();
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
            try
            {
                model.SetInput(TextBoxPath);
                model.SetOutput(TextBoxPath1);

                foreach (var test in model2.Items)
                {
                    if (test.IsChecked)
                        Task.Run(() => model.SearchFilesAsyn(test.Catalog, test.Extension, FileManager.FileMode.Copy));
                }
            }
            catch (Exception e)
            {
                model.SetMessage(e.Message);
                throw;
            }

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

            foreach (var test in model2.Items)
            {
                if (test.IsChecked)
                    Task.Run(() => model.SearchFilesAsyn(test.Catalog, test.Extension, FileManager.FileMode.Move));
            }
            model.SetMessage("Работа завершилась!");
        }

        #endregion
    }
}
