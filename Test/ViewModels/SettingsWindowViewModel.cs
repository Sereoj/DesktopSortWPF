using System.Windows;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models.Settings;
using Test.ViewModels.Base;
using Test.Views.Controls;

namespace Test.ViewModels
{
    internal class SettingsWindowViewModel : ViewModel
    {

        #region Values

        private readonly FirstSettings _firstSettings = new FirstSettings();
        private readonly SecondSettings _secondSettings = new SecondSettings();
        private readonly InfoSettings _infoSettings = new InfoSettings();

        private readonly SettingsModel _model;

        private string _updateTextDirectory;

        public string UpdateTextDirectory
        {
            get => _updateTextDirectory;
            set => Set(ref _updateTextDirectory, value);
        }

        private string _updateTextExtension;
        public string UpdateTextExtension { get => _updateTextExtension; set => Set(ref _updateTextExtension, value); }

        private object _selectedItem;

        public object SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }

        #endregion

        #region Commands

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
                case "first":
                    SelectedItem = _firstSettings;
                    break;
                case "second":
                    SelectedItem = _secondSettings;
                    break;
                case "info":
                    SelectedItem = _infoSettings;
                    break;
            }
        }

        #endregion

        #region UpdateCheckBox

        public ICommand UpdateCheckBox { get; }

        private bool CanUpdateCheckBoxCommandExecute(object p)
        {
            return true;
        }

        private void OnUpdateCheckBoxCommandExecuted(object p)
        {
            switch (p)
            {
                case "word":
                    UpdateTextDirectory = _model.Items[0].Catalog;
                    UpdateTextExtension = _model.Items[0].Extension;
                    break;
                case "excel":
                    UpdateTextDirectory = _model.Items[1].Catalog;
                    UpdateTextExtension = _model.Items[1].Extension;
                    break;
                case "access":
                    UpdateTextDirectory = _model.Items[2].Catalog;
                    UpdateTextExtension = _model.Items[2].Extension;
                    break;
                case "project":
                    UpdateTextDirectory = _model.Items[3].Catalog;
                    UpdateTextExtension = _model.Items[3].Extension;
                    break;
                case "point":
                    UpdateTextDirectory = _model.Items[4].Catalog;
                    UpdateTextExtension = _model.Items[4].Extension;
                    break;
                case "note":
                    UpdateTextDirectory = _model.Items[5].Catalog;
                    UpdateTextExtension = _model.Items[5].Extension;
                    break;
                case "image":
                    UpdateTextDirectory = _model.Items[6].Catalog;
                    UpdateTextExtension = _model.Items[6].Extension;
                    break;
                case "media":
                    UpdateTextDirectory = _model.Items[7].Catalog;
                    UpdateTextExtension = _model.Items[7].Extension;
                    break;
                case "archive":
                    UpdateTextDirectory = _model.Items[8].Catalog;
                    UpdateTextExtension = _model.Items[8].Extension;
                    break;
                case "archive1":
                    UpdateTextDirectory = _model.Items[9].Catalog;
                    UpdateTextExtension = _model.Items[9].Extension;
                    break;
                case "archive2":
                    UpdateTextDirectory = _model.Items[10].Catalog;
                    UpdateTextExtension = _model.Items[10].Extension;
                    break;
            }
        }
        #endregion

        #endregion

        public SettingsWindowViewModel()
        {
            //По умолчанию first
            OnPageButtonCommandExecuted("first");
            _model = SettingsModel.Instance;
            #region Commands

            PageButtonCommand = new RelayCommand(OnPageButtonCommandExecuted, CanPageButtonCommandExecute);
            UpdateCheckBox = new RelayCommand(OnUpdateCheckBoxCommandExecuted, CanUpdateCheckBoxCommandExecute);
            #endregion
        }
    }
}