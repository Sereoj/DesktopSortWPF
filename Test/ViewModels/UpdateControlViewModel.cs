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

        private Visibility _VisibilityUpdate;
        private bool _isLoading;
        public ViewModelCollection listVM
        {
            get; set;
        }
        private ModelCollection modelCollection;

        public Visibility VisibilityUpdate
        {
            get => _VisibilityUpdate;
            set => Set(ref _VisibilityUpdate, value);
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
            Task.Run(() => listVM.UpdaterVM.Download());
        }


        public void Init()
        {
            throw new System.NotImplementedException();
        }

        public UpdateControlViewModel()
        {
        }

        public UpdateControlViewModel(ViewModelCollection listVM, ModelCollection modelCollection)
        {
            this.listVM = listVM;
            this.modelCollection = modelCollection;

            VisibilityUpdate = Visibility.Visible;

            UpdateApplicationButton = new RelayCommand(OnUpdateApplicationButtonExecuted, CanUpdateApplicationButtonExecute);
        }
    }
}
