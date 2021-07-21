using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DesktopSort.UI.Infrastucture.Commands;
using DesktopSort.UI.Models;
using DesktopSort.UI.ViewModels.Base;
using DesktopSort.UI.Views.Controls;

namespace DesktopSort.UI.ViewModels
{
    public class SettingsWindowViewModel : ViewModel
    {
        private ICommand _PageButtonCommand;

        public SettingsWindowViewModel()
        {
        }

        public SettingsWindowViewModel(ViewModelCollection listVM, ModelCollection modelCollection)
        {
            ListVM = listVM;
            ModelCollection = modelCollection;

            FirstSettings = new FirstSettings(modelCollection);
            InfoSettings = new InfoSettings(modelCollection);
            SecondSettings = new SecondSettings(modelCollection);
            UpdateContol = new UpdateControl(modelCollection);
            Develop = new Develop(modelCollection);

            FirstSettingsViewModel = new FirstSettingsViewModel(ListVM, ModelCollection);
            SecondSettingViewModel = new SecondSettingViewModel(ListVM, ModelCollection);
            InfoSettingsViewModel = new InfoSettingsViewModel(ListVM, ModelCollection);
            UpdateControlViewModel = new UpdateControlViewModel(ListVM, ModelCollection);
            DevelopSettingsVM = new DevelopSettingsVM(ListVM, ModelCollection);

            ListVM.UpdateControlViewModel = UpdateControlViewModel;

            VisibilityDev = Visibility.Hidden;
            VisibilityUpdate = Visibility.Hidden;
            OnPageButtonCommandExecuted("first");

            if (ModelCollection.SettingsModel.Advanced.AdvancedConfig.Mode == ApplicationNavigationMode.Dev)
                VisibilityDev = Visibility.Visible;
        }

        public ICommand PageButtonCommand => _PageButtonCommand ??=
            new RelayCommand(OnPageButtonCommandExecuted, CanPageButtonCommandExecute);

        private bool CanPageButtonCommandExecute(object p)
        {
            return true;
        }

        private void OnPageButtonCommandExecuted(object p)
        {
            switch (p)
            {
                case "first":
                    SelectedView = FirstSettings;
                    SelectedView.DataContext = FirstSettingsViewModel;
                    break;
                case "second":
                    SelectedView = SecondSettings;
                    SelectedView.DataContext = SecondSettingViewModel;
                    break;
                case "info":
                    SelectedView = InfoSettings;
                    SelectedView.DataContext = InfoSettingsViewModel;
                    break;
                case "update":
                    SelectedView = UpdateContol;
                    SelectedView.DataContext = UpdateControlViewModel;
                    break;
                case "develop":
                    SelectedView = Develop;
                    SelectedView.DataContext = DevelopSettingsVM;
                    break;
            }
        }

        public void SetPage(string page)
        {
            OnPageButtonCommandExecuted(page);
        }

        #region Values

        private FirstSettings FirstSettings
        {
            get;
        }

        private SecondSettings SecondSettings
        {
            get;
        }

        private InfoSettings InfoSettings
        {
            get;
        }

        private UpdateControl UpdateContol
        {
            get;
        }

        private Develop Develop
        {
            get;
        }

        private Visibility visibility;

        public Visibility VisibilityUpdate
        {
            get => visibility;
            set => Set(ref visibility, value);
        }

        private Visibility visibilityDev;

        public Visibility VisibilityDev
        {
            get => visibilityDev;
            set => Set(ref visibilityDev, value);
        }

        private UserControl _SelectedView;

        public UserControl SelectedView
        {
            get => _SelectedView;
            set => Set(ref _SelectedView, value);
        }

        public ViewModelCollection ListVM
        {
            get;
        }

        public ModelCollection ModelCollection
        {
            get;
        }

        public FirstSettingsViewModel FirstSettingsViewModel
        {
            get;
            set;
        }

        public SecondSettingViewModel SecondSettingViewModel
        {
            get;
            set;
        }

        public InfoSettingsViewModel InfoSettingsViewModel
        {
            get;
            set;
        }

        public UpdateControlViewModel UpdateControlViewModel
        {
            get;
            set;
        }

        public DevelopSettingsVM DevelopSettingsVM
        {
            get;
            set;
        }

        #endregion
    }
}