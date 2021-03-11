using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models.Settings;
using Test.Models.GLUpdater;
using Test.ViewModels.Base;
using Test.Views.Controls;

namespace Test.ViewModels
{
    internal class SettingsWindowViewModel : ViewModel
    {

        #region Values

        private readonly FirstSettings _firstSettings;
        private readonly SecondSettings _secondSettings;
        private readonly InfoSettings _infoSettings;
        private readonly UpdateControl _updateContol;


        private Visibility visibility;
        private object _selectedItem;

        public Visibility VisibilityUpdate
        {
            get => visibility;
            set => Set(ref visibility, value);
        }
        public object SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }

        #endregion

        public SettingsWindowViewModel()
        {

            _firstSettings = new FirstSettings();
            _infoSettings = new InfoSettings();
            _secondSettings = new SecondSettings();
            _updateContol = new UpdateControl();

            //По умолчанию first
            OnPageButtonCommandExecuted("first");

            #region Commands
            PageButtonCommand = new RelayCommand(OnPageButtonCommandExecuted, CanPageButtonCommandExecute);
            #endregion
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



        #endregion


    }
}