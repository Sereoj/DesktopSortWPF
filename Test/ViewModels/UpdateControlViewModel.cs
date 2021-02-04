using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models.Settings;
using Test.Services.GLUpdater;
using Test.ViewModels.Base;
using Test.Views.Controls;

namespace Test.ViewModels
{
    internal class UpdateControlViewModel : ViewModel
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

        private string _VisibilityUpdate;

        public string VisibilityUpdate
        {
            get => _VisibilityUpdate;
            set => Set(ref _VisibilityUpdate, value);
        }

        public ICommand UpdateApplicationButton { get; }

        private bool CanUpdateApplicationButtonExecute(object p)
        {
            return true;
        }

        private void OnUpdateApplicationButtonExecuted(object p)
        {
            GLUpdater.Model.GetNewApplication();       
        }

        public UpdateControlViewModel()
        {
            VisibilityUpdate = "Visible";
            UpdateInformation = GLUpdater.Model.GetResult();

            UpdateApplicationButton = new RelayCommand(OnUpdateApplicationButtonExecuted, CanUpdateApplicationButtonExecute);
        }
    }
}
