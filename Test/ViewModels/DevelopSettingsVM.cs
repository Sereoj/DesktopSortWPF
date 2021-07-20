using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models;
using Test.Models.Settings;
using Test.Resources.Localization;
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
        public ICommand UpdateButtonCommand => _UpdateButtonCommand ??= ( _UpdateButtonCommand = new RelayCommand(OnUpdateButtonCommandExecuted, CanUpdateButtonCommandExecute) );

        private bool CanUpdateButtonCommandExecute(object p) => true;

        private void OnUpdateButtonCommandExecuted(object p)
        {
            ListVM.SettingsWindowViewModel.VisibilityUpdate = System.Windows.Visibility.Visible;
            ListVM.UpdateControlViewModel.VisibilityUpdate = System.Windows.Visibility.Visible;

            ListVM.MessengerVM.SetMessage("Открыт доступ к принудительному обновлению");
        }

        private ICommand _StandardSettingsCommand;
        public ICommand StandardSettingsCommand => _StandardSettingsCommand ??= ( _StandardSettingsCommand = new RelayCommand(OnStandardSettingsCommandExecuted, CanStandardSettingsCommandExecute) );

        private bool CanStandardSettingsCommandExecute(object p) => true;

        private void OnStandardSettingsCommandExecuted(object p)
        {
            ModelCollection.SettingsModel.Default();
        }

        private ICommand _UserModeCommand;
        public ICommand UserModeCommand => _UserModeCommand ??= ( _UserModeCommand = new RelayCommand(OnUserModeCommandExecuted, CanUserModeCommandExecute) );

        private bool CanUserModeCommandExecute(object p) => true;

        private void OnUserModeCommandExecuted(object p)
        {
            ListVM.SettingsWindowViewModel.VisibilityDev = System.Windows.Visibility.Hidden; 
            ModelCollection.SettingsModel.Advanced.AdvancedConfig.Mode = ApplicationNavigationMode.Main;

            ModelCollection.SettingsModel.Update(ModelCollection.SettingsModel);
        }

        private ICommand _SendMessageCommand;
        public ICommand SendMessageCommand => _SendMessageCommand ??= (_SendMessageCommand = new RelayCommand(OnSendMessageCommandExecuted, CanSendMessageCommandExecute));

        private bool CanSendMessageCommandExecute(object p) => true;

        private void OnSendMessageCommandExecuted(object p)
        {
            var random = new Random().Next(0,100);
            ListVM.MessengerVM.SetMessage("MessageWelcomeNVersion", random.ToString());
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
