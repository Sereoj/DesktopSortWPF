using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DesktopSort.UI.Infrastucture.Commands;
using DesktopSort.UI.Models;
using DesktopSort.UI.ViewModels.Base;

namespace DesktopSort.UI.ViewModels
{
    public class UpdateControlViewModel : ViewModel, IApplicationContentView
    {
        private bool _isLoading;
        private string _updateInformation;

        private Visibility _visibilityUpdate;

        public ModelCollection ModelCollection;

        public UpdateControlViewModel()
        {
        }

        public UpdateControlViewModel(ViewModelCollection listVm, ModelCollection modelCollection)
        {
            ListVm = listVm;
            ModelCollection = modelCollection;

            VisibilityUpdate = Visibility.Visible;

            UpdateApplicationButton =
                new RelayCommand(OnUpdateApplicationButtonExecuted, CanUpdateApplicationButtonExecute);
        }

        public string UpdateInformation
        {
            get => _updateInformation;
            set => Set(ref _updateInformation, value);
        }

        public ViewModelCollection ListVm
        {
            get;
            set;
        }

        public Visibility VisibilityUpdate
        {
            get => _visibilityUpdate;
            set => Set(ref _visibilityUpdate, value);
        }

        public ICommand UpdateApplicationButton
        {
            get;
        }

        public string Name => "UpdateControlTitle";

        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }


        public void Init()
        {
            throw new NotImplementedException();
        }


        private bool CanUpdateApplicationButtonExecute(object p)
        {
            return true;
        }

        private void OnUpdateApplicationButtonExecuted(object p)
        {
            Task.Run(() => ListVm.UpdaterVM.Download());
        }
    }
}