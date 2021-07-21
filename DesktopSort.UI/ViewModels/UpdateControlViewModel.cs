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
        private string _updateInformation;
        public string UpdateInformation
        {
            get => _updateInformation;
            set
            {
                Set(ref _updateInformation, value);
            }
        }

        private Visibility _visibilityUpdate;
        private bool _isLoading;
        public ViewModelCollection ListVm
        {
            get; set;
        }

        public ModelCollection ModelCollection;

        public Visibility VisibilityUpdate
        {
            get => _visibilityUpdate;
            set => Set(ref _visibilityUpdate, value);
        }

        public string Name => "UpdateControlTitle";

        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }

        public ICommand UpdateApplicationButton { get; }


        private bool CanUpdateApplicationButtonExecute(object p)
        {
            return true;
        }

        private void OnUpdateApplicationButtonExecuted(object p)
        {
            Task.Run(() => ListVm.UpdaterVM.Download());
        }


        public void Init()
        {
            throw new System.NotImplementedException();
        }

        public UpdateControlViewModel()
        {
        }

        public UpdateControlViewModel(ViewModelCollection listVm, ModelCollection modelCollection)
        {
            this.ListVm = listVm;
            this.ModelCollection = modelCollection;

            VisibilityUpdate = Visibility.Visible;

            UpdateApplicationButton = new RelayCommand(OnUpdateApplicationButtonExecuted, CanUpdateApplicationButtonExecute);
        }
    }
}
