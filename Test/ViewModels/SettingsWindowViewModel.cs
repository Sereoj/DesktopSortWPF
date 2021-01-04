using System;
using System.Windows;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models;
using Test.ViewModels.Base;
using Test.Views.Controls;

namespace Test.ViewModels
{
    internal class SettingsWindowViewModel : ViewModel
    {
        #region Values

        private FirstSettings FirstSettings = new FirstSettings();
        private SecondSettings SecondSettings = new SecondSettings();
        private InfoSettings InfoSettings = new InfoSettings();

        private object _SelectedItem;
        public object SelectedItem { get => _SelectedItem; set => Set(ref _SelectedItem, value); }
        #endregion

        #region Commands

        #region PageButtonCommand
        public ICommand PageButtonCommand { get; }

        private bool CanPageButtonCommandExecute(object p) => true;
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

        #endregion

        public SettingsWindowViewModel()
        {
            //По умоланию first
            OnPageButtonCommandExecuted("first");
            #region Commands
            PageButtonCommand = new RelayCommand(OnPageButtonCommandExecuted, CanPageButtonCommandExecute);
            #endregion
        }
    }
}
