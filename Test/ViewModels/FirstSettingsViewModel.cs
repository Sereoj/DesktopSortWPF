using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models;
using Test.Models.Settings;
using Test.Resources.Localization;
using Test.ViewModels.Base;

namespace Test.ViewModels
{
    public class FirstSettingsViewModel : ViewModel, IApplicationContentView
    {
        #region values
        public SettingsModel Model { get; set; }
        public CheckBox LastActiveCheckBox { get; set; }

        public string IconPath { get; set; }

        private string _updateTextDirectory;
        public string UpdateTextDirectory
        {
            get => _updateTextDirectory;
            set
            {
                Set(ref _updateTextDirectory, value);
            }
        }


        private string _updateTextExtension;
        public string UpdateTextExtension
        {
            get => _updateTextExtension;
            set => Set(ref _updateTextExtension, value);
        }

        private bool _onlyOldFiles;
        public bool OnlyOldFiles
        {
            get => _onlyOldFiles;
            set => Set(ref _onlyOldFiles, value);
        }

        private bool _onlyNewFiles;
        public bool OnlyNewFiles
        {
            get => _onlyNewFiles;
            set => Set(ref _onlyNewFiles, value);
        }

        private bool _onlyDuplicateFiles;
        public bool OnlyDuplicateFiles
        {
            get => _onlyDuplicateFiles;
            set => Set(ref _onlyDuplicateFiles, value);
        }

        public string Name => Localization.FirstSettingsTitle;

        private bool _isLoading;

        public ViewModelCollection ListVM
        {
            get;
            set;
        }
        public ModelCollection ModelCollection
        {
            get; set;
        }

        public bool IsLoading { get => _isLoading; set => Set(ref _isLoading, value); }
        #endregion

        #region Commands
        private ICommand _ButtonIconChanger;
        public ICommand ButtonIconChanger => _ButtonIconChanger ?? ( _ButtonIconChanger = new RelayCommand(OnButtonIconChangerExecuted, CanButtonIconChangerExecute) );
        public bool CanButtonIconChangerExecute(object p) => true;
        public void OnButtonIconChangerExecuted(object p)
        {
            using ( System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog() )
            {
                openFileDialog.Filter = "icon files (*.ico)|*.ico";
                if ( openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK )
                {
                    @IconPath = openFileDialog.FileName;
                }
            }
        }
        #region ButtonSaveCommand
        private ICommand _ButtonSaveCommand;
        public ICommand ButtonSaveCommand => _ButtonSaveCommand ?? ( _ButtonSaveCommand = new RelayCommand(OnButtonSaveCommandExecuted, CanButtonSaveCommandExecute) );
        private bool CanButtonSaveCommandExecute(object p) => true;
        private void OnButtonSaveCommandExecuted(object p)
        {
            UpdateSettings(LastActiveCheckBox);
            Model.Update(Model);
        }

        #endregion

        #region UpdateCheckBox
        private ICommand _UpdateCheckBox;
        public ICommand UpdateCheckBox => _UpdateCheckBox ?? ( _UpdateCheckBox = new RelayCommand(OnUpdateCheckBoxCommandExecuted, CanUpdateCheckBoxCommandExecute) );
        private bool CanUpdateCheckBoxCommandExecute(object p) => true;

        private void OnUpdateCheckBoxCommandExecuted(object p)
        {
            var checkbox = p as CheckBox;
            var isChecked = checkbox?.IsChecked;
            var matches = Regex.Matches(checkbox.Name, @"(\d+)");
            var id = int.Parse(matches[0].Groups[1].Value) - 1;
            var item = Model.Items[id];
            LastActiveCheckBox = checkbox;
            UpdateTextDirectory = item.Catalog;
            UpdateTextExtension = item.Extension;
            IconPath = item.IconPath;
            OnlyOldFiles = item.OnlyOldFiles;
            OnlyNewFiles = item.OnlyNewFiles;
            OnlyDuplicateFiles = item.OnlyDuplicateFiles;
            if ( isChecked != null ) item.IsChecked = ( bool )isChecked;
        }
        #endregion 
        #endregion

        public FirstSettingsViewModel()
        {
        }

        public FirstSettingsViewModel(ViewModelCollection listVM, ModelCollection modelCollection)
        {
            ListVM = listVM;
            ModelCollection = modelCollection;

            Model = modelCollection.SettingsModel;

        }

        private void UpdateSettings(CheckBox lastActiveCheckBox)
        {
            if(lastActiveCheckBox != null)
            {
                var matches = Regex.Matches(lastActiveCheckBox.Name, @"(\d+)");
                var id = int.Parse(matches[0].Groups[1].Value) - 1;
                var item = Model.Items[id];

                item.Catalog = UpdateTextDirectory;
                item.Extension = UpdateTextExtension;
                item.IconPath = IconPath;
                item.OnlyOldFiles = OnlyOldFiles;
                item.OnlyNewFiles = OnlyNewFiles;
                item.OnlyDuplicateFiles = OnlyDuplicateFiles;
                item.IsChecked = ( bool )LastActiveCheckBox.IsChecked;
            }
        }

        public void Init()
        {
           // throw new System.NotImplementedException();
        }
    }
}
