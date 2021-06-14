using System.Windows;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models;
using Test.ViewModels.Base;
using Test.Views.Controls;

namespace Test.ViewModels
{
    public class SettingsWindowViewModel : ViewModel
    {

        #region Values
        private readonly FirstSettings _firstSettings;
        private readonly SecondSettings _secondSettings;
        private readonly InfoSettings _infoSettings;
        private readonly UpdateControl _updateContol;


        private Visibility visibility;
        public Visibility VisibilityUpdate
        {
            get => visibility;
            set => Set(ref visibility, value);
        }
        private object _selectedItem;
        public object SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }

        public FirstSettingsViewModel FirstSettingsViewModel { get; set; }
        public SecondSettingViewModel SecondSettingViewModel { get; set; }
        public InfoSettingsViewModel InfoSettingsViewModel { get; set; }
        public UpdateControlViewModel UpdateControlViewModel { get; set; }

        #endregion



        #region Commands

        #region PageButtonCommand

        public ICommand PageButtonCommand { get; }
        public ViewModelCollection ListVM
        {
            get;
            private set;
        }
        public ModelCollection ModelCollection
        {
            get;
            private set;
        }

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

        public SettingsWindowViewModel()
        {
            OnPageButtonCommandExecuted("first");

            _firstSettings = new FirstSettings();
            _infoSettings = new InfoSettings();
            _secondSettings = new SecondSettings();
            _updateContol = new UpdateControl();

            #region Commands
            PageButtonCommand = new RelayCommand(OnPageButtonCommandExecuted, CanPageButtonCommandExecute);
            #endregion
        }

        public SettingsWindowViewModel(ViewModelCollection listVM, ModelCollection modelCollection)
        {

            ListVM = listVM;
            ModelCollection = modelCollection;

            OnPageButtonCommandExecuted("first");

            FirstSettingsViewModel = new FirstSettingsViewModel(ListVM, ModelCollection);
            SecondSettingViewModel = new SecondSettingViewModel(ListVM, ModelCollection);
            InfoSettingsViewModel = new InfoSettingsViewModel(ListVM, ModelCollection);
            UpdateControlViewModel = new UpdateControlViewModel(ListVM, ModelCollection);

            _firstSettings = new FirstSettings(ModelCollection);
            _infoSettings = new InfoSettings(ModelCollection);
            _secondSettings = new SecondSettings(ModelCollection);
            _updateContol = new UpdateControl(ModelCollection);
            
            #region Commands
            PageButtonCommand = new RelayCommand(OnPageButtonCommandExecuted, CanPageButtonCommandExecute);
            #endregion
        }

    }
}