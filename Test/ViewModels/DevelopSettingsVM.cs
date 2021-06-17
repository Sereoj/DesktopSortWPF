﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models;
using Test.Models.Settings;
using Test.ViewModels.Base;

namespace Test.ViewModels
{
    public class DevelopSettingsVM : ViewModel
    {
        public ViewModelCollection ListVM
        {
            get;
        }
        public ModelCollection ModelCollection
        {
            get;
        }

        private ICommand _UpdateButtonCommand;
        public ICommand UpdateButtonCommand => _UpdateButtonCommand ?? ( _UpdateButtonCommand = new RelayCommand(OnUpdateButtonCommandExecuted, CanUpdateButtonCommandExecute) );

        private bool CanUpdateButtonCommandExecute(object p) => true;

        private void OnUpdateButtonCommandExecuted(object p)
        {
            ListVM.SettingsWindowViewModel.VisibilityUpdate = System.Windows.Visibility.Visible;
            ListVM.UpdateControlViewModel.VisibilityUpdate = System.Windows.Visibility.Visible;
        }

        private ICommand _StandardSettingsCommand;
        public ICommand StandardSettingsCommand => _StandardSettingsCommand ?? ( _StandardSettingsCommand = new RelayCommand(OnStandardSettingsCommandExecuted, CanStandardSettingsCommandExecute) );

        private bool CanStandardSettingsCommandExecute(object p) => true;

        private void OnStandardSettingsCommandExecuted(object p)
        {
            ModelCollection.SettingsModel.Default();
        }

        private ICommand _UserModeCommand;
        public ICommand UserModeCommand => _UserModeCommand ?? ( _UserModeCommand = new RelayCommand(OnUserModeCommandExecuted, CanUserModeCommandExecute) );

        private bool CanUserModeCommandExecute(object p) => true;

        private void OnUserModeCommandExecuted(object p)
        {
            ListVM.SettingsWindowViewModel.VisibilityDev = System.Windows.Visibility.Hidden; 
            ModelCollection.SettingsModel.Advanced.AdvancedConfig.Mode = ApplicationNavigationMode.Main;
        }

        public DevelopSettingsVM()
        {
        }
        public DevelopSettingsVM(ViewModelCollection listVM, ModelCollection modelCollection)
        {
            ListVM = listVM;
            ModelCollection = modelCollection;
        }
    }
}