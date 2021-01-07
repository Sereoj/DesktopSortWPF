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

        private readonly FirstSettings FirstSettings = new FirstSettings();
        private readonly SecondSettings SecondSettings = new SecondSettings();
        private readonly InfoSettings InfoSettings = new InfoSettings();

        private SettingsModel Model;

        private object _SelectedItem;

        public object SelectedItem
        {
            get => _SelectedItem;
            set => Set(ref _SelectedItem, value);
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
                    SelectedItem = FirstSettings;
                    break;
                case "second":
                    SelectedItem = SecondSettings;
                    break;
                case "info":
                    SelectedItem = InfoSettings;
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
                    MessageBox.Show(Model.Logger.ToString());
                    break;
                case "second":
                    SelectedItem = SecondSettings;
                    break;
                case "info":
                    SelectedItem = InfoSettings;
                    break;
            }
        }

        #endregion

        #endregion

        public SettingsWindowViewModel()
        {
            //По умолчанию first
            OnPageButtonCommandExecuted("first");
            Model = SettingsModel.Instance;
            #region Commands

            PageButtonCommand = new RelayCommand(OnPageButtonCommandExecuted, CanPageButtonCommandExecute);
            UpdateCheckBox = new RelayCommand(OnUpdateCheckBoxCommandExecuted, CanUpdateCheckBoxCommandExecute);
            #endregion
        }
    }
}