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
    internal class FirstSettingsViewModel : ViewModel
    {
        #region values
        private SettingsModel Model { get; set; }
        private CheckBox LastActiveCheckBox { get; set; }
        private string _updateTextDirectory;
        public string UpdateTextDirectory
        {
            get => _updateTextDirectory;
            set => Set(ref _updateTextDirectory, value);
        }


        private string _updateTextExtension;
        public string UpdateTextExtension
        {
            get => _updateTextExtension;
            set => Set(ref _updateTextExtension, value);
        }
        #endregion

        #region Commands

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
        #endregion

        public FirstSettingsViewModel()
        {
            Model = SettingsModel.Instance;
            LoadCheckPoint();

            UpdateCheckBox = new RelayCommand(OnUpdateCheckBoxCommandExecuted, CanUpdateCheckBoxCommandExecute);
            ButtonSaveCommand = new RelayCommand(OnButtonSaveCommandExecuted, CanButtonSaveCommandExecute);
        }

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
        }
    }
}
