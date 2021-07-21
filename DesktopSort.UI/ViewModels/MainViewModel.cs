using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using DesktopSort.UI.Infrastucture.Commands;
using DesktopSort.UI.Models;
using DesktopSort.UI.Models.Settings;
using DesktopSort.UI.ViewModels.Base;
using WK.Libraries.BetterFolderBrowserNS;
using static DesktopSort.UI.ViewModels.FileManagerVM;

namespace DesktopSort.UI.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private ICommand _FileDialogButtonCommand;

        private Visibility _IgnoreFilesVisibility;
        private string _TextBoxPath;
        private string _TextBoxPath1;

        public MainViewModel()
        {
            IgnoreFilesVisibility = Visibility.Hidden;
        }

        public MainViewModel(ViewModelCollection listVM, ModelCollection modelCollection)
        {
            ListVM = listVM;
            ModelCollection = modelCollection;

            SettingsModel = modelCollection.SettingsModel;

            var settings = SettingsModel.Advanced.AdvancedConfig;

            var InputPath = settings.InputPath;
            var OutputPath = settings.OutputPath;

            TextBoxPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            TextBoxPath1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            IgnoreFilesVisibility = Visibility.Hidden;


            if (settings.Mode == ApplicationNavigationMode.Dev) IgnoreFilesVisibility = Visibility.Visible;

            // Костыль
            if (!string.IsNullOrEmpty(InputPath) && !string.IsNullOrEmpty(OutputPath))
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

        public ViewModelCollection ListVM
        {
            get;
            set;
        }

        public ModelCollection ModelCollection
        {
            get;
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

        public Visibility IgnoreFilesVisibility
        {
            get => _IgnoreFilesVisibility;
            set => Set(ref _IgnoreFilesVisibility, value);
        }

        public SettingsModel SettingsModel
        {
            get;
        }

        public ICommand FileDialogButtonCommand => _FileDialogButtonCommand ??=
            new RelayCommand(OnFileDialogButtonCommandExecuted, CanFileDialogButtonCommandExecute);

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

        #region CopyButtonCommand

        private ICommand _CopyButtonCommand;

        public ICommand CopyButtonCommand => _CopyButtonCommand ??=
            new RelayCommand(OnCopyButtonCommandExecuted, CanCopyButtonCommandExecute);

        private bool CanCopyButtonCommandExecute(object p)
        {
            return true;
        }

        private void OnCopyButtonCommandExecuted(object p)
        {
            var fileManager = ListVM.FileManagerVM;
            var messenger = ListVM.MessengerVM;

            fileManager.SetInput(TextBoxPath);
            fileManager.SetOutput(TextBoxPath1);

            foreach (var config in SettingsModel.Items)
                if (config.IsChecked)
                    Task.Run(() => fileManager.SearchFilesAsyn(config, FileMode.Copy));
            fileManager.DeleteDirectory(TextBoxPath, SettingsModel.Advanced.AdvancedConfig.DeleteDefaultDirectory);
            messenger.SetMessage("WorkCompleted");
        }

        #endregion

        #region CutButtonCommand

        private ICommand _CutButtonCommand;

        public ICommand CutButtonCommand => _CutButtonCommand ??=
            new RelayCommand(OnCutButtonCommandExecuted, CanCutButtonCommandExecute);

        private bool CanCutButtonCommandExecute(object p)
        {
            return true;
        }

        private void OnCutButtonCommandExecuted(object p)
        {
            var fileManager = ListVM.FileManagerVM;
            var messenger = ListVM.MessengerVM;

            fileManager.SetInput(TextBoxPath);
            fileManager.SetOutput(TextBoxPath1);

            foreach (var config in SettingsModel.Items)
                if (config.IsChecked)
                    Task.Run(() => fileManager.SearchFilesAsyn(config, FileMode.Move));
            fileManager.DeleteDirectory(TextBoxPath, SettingsModel.Advanced.AdvancedConfig.DeleteDefaultDirectory);
            messenger.SetMessage("WorkCompleted");
        }

        #endregion

        #region IgnoreButtonCommand

        private ICommand _IgnoreButtonCommand;

        public ICommand IgnoreButtonCommand => _IgnoreButtonCommand ??=
            new RelayCommand(OnIgnoreButtonCommandExecuted, CanIgnoreButtonCommandExecute);

        private bool CanIgnoreButtonCommandExecute(object p)
        {
            return true;
        }

        private void OnIgnoreButtonCommandExecuted(object p)
        {
            var fileManager = ListVM.FileManagerVM;
            var messenger = ListVM.MessengerVM;

            fileManager.SetInput(TextBoxPath);
            fileManager.SetOutput(TextBoxPath1);

            foreach (var config in SettingsModel.Items)
                if (config.IsChecked)
                    Task.Run(() => fileManager.SearchFilesAsyn(config, FileMode.Ignore));
            fileManager.DeleteDirectory(TextBoxPath, SettingsModel.Advanced.AdvancedConfig.DeleteDefaultDirectory);
            messenger.SetMessage("WorkCompleted");
        }

        #endregion
    }
}