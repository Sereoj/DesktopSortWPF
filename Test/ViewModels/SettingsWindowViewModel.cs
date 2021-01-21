using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models.Settings;
using Test.Services.GLUpdater;
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
        private readonly UpdateControl _updateContol = new UpdateControl();
        private SettingsModel Model { get; set; }

        private string _updateTextDirectory;

        private CheckBox LastActiveCheckBox { get; set; }

        public string UpdateTextDirectory
        {
            get => _updateTextDirectory;
            set
            {
                Set(ref _updateTextDirectory, value);
                //UpdateSettings(LastActiveCheckBox);
            }
        }


        private string _updateTextExtension;
        public string UpdateTextExtension
        {
            get => _updateTextExtension;
            set
            {
                Set(ref _updateTextExtension, value);
                //UpdateSettings(LastActiveCheckBox);
            }
        }

        private string _updateInformation;
        public string UpdateInformation
        {
            get => _updateInformation;
            set
            {
                Set(ref _updateInformation, value);
            }
        }

        private object _selectedItem;


        public object SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }

        #endregion


        private void UpdateSettings(CheckBox lastActiveCheckBox)
        {

            switch (lastActiveCheckBox.Name)
            {
                case "CheckFormat1":
                    Model.Items[0].Catalog = UpdateTextDirectory;
                    Model.Items[0].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat2":
                    Model.Items[1].Catalog = UpdateTextDirectory;
                    Model.Items[1].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat3":
                    Model.Items[2].Catalog = UpdateTextDirectory;
                    Model.Items[2].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat4":
                    Model.Items[3].Catalog = UpdateTextDirectory;
                    Model.Items[3].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat5":
                    Model.Items[4].Catalog = UpdateTextDirectory;
                    Model.Items[4].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat6":
                    Model.Items[5].Catalog = UpdateTextDirectory;
                    Model.Items[5].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat7":
                    Model.Items[6].Catalog = UpdateTextDirectory;
                    Model.Items[6].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat8":
                    Model.Items[7].Catalog = UpdateTextDirectory;
                    Model.Items[7].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat9":
                    Model.Items[8].Catalog = UpdateTextDirectory;
                    Model.Items[8].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat10":
                    Model.Items[9].Catalog = UpdateTextDirectory;
                    Model.Items[9].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat11":
                    Model.Items[10].Catalog = UpdateTextDirectory;
                    Model.Items[10].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat12":
                    Model.Items[11].Catalog = UpdateTextDirectory;
                    Model.Items[11].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat13":
                    Model.Items[12].Catalog = UpdateTextDirectory;
                    Model.Items[12].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat14":
                    Model.Items[13].Catalog = UpdateTextDirectory;
                    Model.Items[13].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat15":
                    Model.Items[14].Catalog = UpdateTextDirectory;
                    Model.Items[14].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat16":
                    Model.Items[15].Catalog = UpdateTextDirectory;
                    Model.Items[15].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat17":
                    Model.Items[16].Catalog = UpdateTextDirectory;
                    Model.Items[16].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat18":
                    Model.Items[17].Catalog = UpdateTextDirectory;
                    Model.Items[17].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat19":
                    Model.Items[18].Catalog = UpdateTextDirectory;
                    Model.Items[18].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat20":
                    Model.Items[19].Catalog = UpdateTextDirectory;
                    Model.Items[19].Extension = UpdateTextExtension;
                    break;
                case "CheckFormat21":
                    Model.Items[20].Catalog = UpdateTextDirectory;
                    Model.Items[20].Extension = UpdateTextExtension;
                    break;
            }
        }

        private void LoadCheckPoint()
        {
            _firstSettings.CheckFormat1.IsChecked = Model.Items[0].IsChecked;
            _firstSettings.CheckFormat2.IsChecked = Model.Items[1].IsChecked;
            _firstSettings.CheckFormat3.IsChecked = Model.Items[2].IsChecked;
            _firstSettings.CheckFormat4.IsChecked = Model.Items[3].IsChecked;
            _firstSettings.CheckFormat5.IsChecked = Model.Items[4].IsChecked;
            _firstSettings.CheckFormat6.IsChecked = Model.Items[5].IsChecked;
            _firstSettings.CheckFormat7.IsChecked = Model.Items[6].IsChecked;
            _firstSettings.CheckFormat8.IsChecked = Model.Items[7].IsChecked;
            _firstSettings.CheckFormat9.IsChecked = Model.Items[8].IsChecked;
            _firstSettings.CheckFormat10.IsChecked = Model.Items[9].IsChecked;
            _firstSettings.CheckFormat11.IsChecked = Model.Items[10].IsChecked;
            _firstSettings.CheckFormat12.IsChecked = Model.Items[11].IsChecked;
            _firstSettings.CheckFormat13.IsChecked = Model.Items[12].IsChecked;
            _firstSettings.CheckFormat14.IsChecked = Model.Items[13].IsChecked;
            _firstSettings.CheckFormat15.IsChecked = Model.Items[14].IsChecked;
            _firstSettings.CheckFormat16.IsChecked = Model.Items[15].IsChecked;
            _firstSettings.CheckFormat17.IsChecked = Model.Items[16].IsChecked;
            _firstSettings.CheckFormat18.IsChecked = Model.Items[17].IsChecked;
            _firstSettings.CheckFormat19.IsChecked = Model.Items[18].IsChecked;
            _firstSettings.CheckFormat20.IsChecked = Model.Items[19].IsChecked;
            _firstSettings.CheckFormat21.IsChecked = Model.Items[20].IsChecked;
        }

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
                case "update":
                    SelectedItem = _updateContol;
                    break;
            }
        }

        #endregion

        #region ButtonSaveCommand
        public ICommand ButtonSaveCommand { get; }

        private bool CanButtonSaveCommandExecute(object p)
        {
            return true;
        }

        private void OnButtonSaveCommandExecuted(object p)
        {
            UpdateSettings(LastActiveCheckBox);
            //MessageBox.Show(Model.Items[0].Catalog);
            SettingsModel.Update(Model);
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

            var checkbox = p as CheckBox;
            var isChecked = checkbox?.IsChecked;


            LastActiveCheckBox = checkbox;

            switch (checkbox.Name)
            {
                case "CheckFormat1":
                    UpdateTextDirectory = Model.Items[0].Catalog;
                    UpdateTextExtension = Model.Items[0].Extension;
                    if (isChecked != null) Model.Items[0].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat2":
                    UpdateTextDirectory = Model.Items[1].Catalog;
                    UpdateTextExtension = Model.Items[1].Extension;
                    if (isChecked != null) Model.Items[1].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat3":
                    UpdateTextDirectory = Model.Items[2].Catalog;
                    UpdateTextExtension = Model.Items[2].Extension;
                    if (isChecked != null) Model.Items[2].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat4":
                    UpdateTextDirectory = Model.Items[3].Catalog;
                    UpdateTextExtension = Model.Items[3].Extension;
                    if (isChecked != null) Model.Items[3].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat5":
                    UpdateTextDirectory = Model.Items[4].Catalog;
                    UpdateTextExtension = Model.Items[4].Extension;
                    if (isChecked != null) Model.Items[4].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat6":
                    UpdateTextDirectory = Model.Items[5].Catalog;
                    UpdateTextExtension = Model.Items[5].Extension;
                    if (isChecked != null) Model.Items[5].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat7":
                    UpdateTextDirectory = Model.Items[6].Catalog;
                    UpdateTextExtension = Model.Items[6].Extension;
                    if (isChecked != null) Model.Items[6].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat8":
                    UpdateTextDirectory = Model.Items[7].Catalog;
                    UpdateTextExtension = Model.Items[7].Extension;
                    if (isChecked != null) Model.Items[7].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat9":
                    UpdateTextDirectory = Model.Items[8].Catalog;
                    UpdateTextExtension = Model.Items[8].Extension;
                    if (isChecked != null) Model.Items[8].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat10":
                    UpdateTextDirectory = Model.Items[9].Catalog;
                    UpdateTextExtension = Model.Items[9].Extension;
                    if (isChecked != null) Model.Items[9].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat11":
                    UpdateTextDirectory = Model.Items[10].Catalog;
                    UpdateTextExtension = Model.Items[10].Extension;
                    if (isChecked != null) Model.Items[10].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat12":
                    UpdateTextDirectory = Model.Items[11].Catalog;
                    UpdateTextExtension = Model.Items[11].Extension;
                    if (isChecked != null) Model.Items[11].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat13":
                    UpdateTextDirectory = Model.Items[12].Catalog;
                    UpdateTextExtension = Model.Items[12].Extension;
                    if (isChecked != null) Model.Items[12].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat14":
                    UpdateTextDirectory = Model.Items[13].Catalog;
                    UpdateTextExtension = Model.Items[13].Extension;
                    if (isChecked != null) Model.Items[13].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat15":
                    UpdateTextDirectory = Model.Items[14].Catalog;
                    UpdateTextExtension = Model.Items[14].Extension;
                    if (isChecked != null) Model.Items[14].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat16":
                    UpdateTextDirectory = Model.Items[15].Catalog;
                    UpdateTextExtension = Model.Items[15].Extension;
                    if (isChecked != null) Model.Items[15].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat17":
                    UpdateTextDirectory = Model.Items[16].Catalog;
                    UpdateTextExtension = Model.Items[16].Extension;
                    if (isChecked != null) Model.Items[16].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat18":
                    UpdateTextDirectory = Model.Items[17].Catalog;
                    UpdateTextExtension = Model.Items[17].Extension;
                    if (isChecked != null) Model.Items[17].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat19":
                    UpdateTextDirectory = Model.Items[18].Catalog;
                    UpdateTextExtension = Model.Items[18].Extension;
                    if (isChecked != null) Model.Items[0].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat20":
                    UpdateTextDirectory = Model.Items[19].Catalog;
                    UpdateTextExtension = Model.Items[19].Extension;
                    if (isChecked != null) Model.Items[0].IsChecked = (bool)isChecked;
                    break;
                case "CheckFormat21":
                    UpdateTextDirectory = Model.Items[20].Catalog;
                    UpdateTextExtension = Model.Items[20].Extension;
                    if (isChecked != null) Model.Items[0].IsChecked = (bool)isChecked;
                    break;
            }

        }
        #endregion

        public ICommand UpdateApplicationButton { get; }

        private bool CanUpdateApplicationButtonExecute(object p)
        {
            return true;
        }

        private void OnUpdateApplicationButtonExecuted(object p)
        {
            GLUpdater.Model.GetNewApplication();
        }

        #endregion

        public SettingsWindowViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            Model = SettingsModel.Instance;

            //UpdateInformation = GLUpdater.Model.GetInformation();

            //По умолчанию first
            OnPageButtonCommandExecuted("first");

            

            LoadCheckPoint();
            #region Commands

            PageButtonCommand = new RelayCommand(OnPageButtonCommandExecuted, CanPageButtonCommandExecute);
            UpdateCheckBox = new RelayCommand(OnUpdateCheckBoxCommandExecuted, CanUpdateCheckBoxCommandExecute);
            ButtonSaveCommand = new RelayCommand(OnButtonSaveCommandExecuted, CanButtonSaveCommandExecute);
            UpdateApplicationButton = new RelayCommand(OnUpdateApplicationButtonExecuted, CanUpdateApplicationButtonExecute);
            #endregion
        }
    }
}