using System;
using System.Windows;
using System.Windows.Input;
using DesktopSort.UI.Infrastucture.Commands;
using DesktopSort.UI.Models;
using DesktopSort.UI.ViewModels.Base;

namespace DesktopSort.UI.ViewModels
{
    public class DevelopSettingsVM : ViewModel
    {
        private ICommand _SendMessageCommand;

        private ICommand _StandardSettingsCommand;

        private ICommand _UpdateButtonCommand;

        private ICommand _UserModeCommand;

        public DevelopSettingsVM()
        {
        }

        public DevelopSettingsVM(ViewModelCollection listVM, ModelCollection modelCollection)
        {
            ListVM = listVM;
            ModelCollection = modelCollection;
        }

        public ViewModelCollection ListVM
        {
            get;
        }

        public ModelCollection ModelCollection
        {
            get;
        }

        public ICommand UpdateButtonCommand => _UpdateButtonCommand ??= _UpdateButtonCommand =
            new RelayCommand(OnUpdateButtonCommandExecuted, CanUpdateButtonCommandExecute);

        public ICommand StandardSettingsCommand => _StandardSettingsCommand ??= _StandardSettingsCommand =
            new RelayCommand(OnStandardSettingsCommandExecuted, CanStandardSettingsCommandExecute);

        public ICommand UserModeCommand => _UserModeCommand ??=
            _UserModeCommand = new RelayCommand(OnUserModeCommandExecuted, CanUserModeCommandExecute);

        public ICommand SendMessageCommand => _SendMessageCommand ??= _SendMessageCommand =
            new RelayCommand(OnSendMessageCommandExecuted, CanSendMessageCommandExecute);

        private bool CanUpdateButtonCommandExecute(object p)
        {
            return true;
        }

        private void OnUpdateButtonCommandExecuted(object p)
        {
            ListVM.SettingsWindowViewModel.VisibilityUpdate = Visibility.Visible;
            ListVM.UpdateControlViewModel.VisibilityUpdate = Visibility.Visible;
        }

        private bool CanStandardSettingsCommandExecute(object p)
        {
            return true;
        }

        private void OnStandardSettingsCommandExecuted(object p)
        {
            ModelCollection.SettingsModel.Default();
            ModelCollection.IsDefaultSettings = true;
            ModelCollection.IsDefaultSettings = false;
        }

        private bool CanUserModeCommandExecute(object p)
        {
            return true;
        }

        private void OnUserModeCommandExecuted(object p)
        {
            ListVM.SettingsWindowViewModel.VisibilityDev = Visibility.Hidden;
            ModelCollection.SettingsModel.Advanced.AdvancedConfig.Mode = ApplicationNavigationMode.Main;

            ModelCollection.SettingsModel.Update(ModelCollection.SettingsModel);
        }

        private bool CanSendMessageCommandExecute(object p)
        {
            return true;
        }

        private void OnSendMessageCommandExecuted(object p)
        {
            var random = new Random().Next(0, 100);
            ListVM.MessengerVM.SetMessage("MessageWelcomeNVersion", random.ToString());
        }
    }
}