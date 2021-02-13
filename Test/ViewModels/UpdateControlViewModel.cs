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
    internal class UpdateControlViewModel : ViewModel, IApplicationContentView
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
        private bool _isLoading;

        public string VisibilityUpdate
        {
            get => _VisibilityUpdate;
            set => Set(ref _VisibilityUpdate, value);
        }

        public string Name => "Настройки // Обновление";

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
            GLUpdater.Model.GetNewApplication();       
        }

        public void Init()
        {
            throw new System.NotImplementedException();
        }

        public UpdateControlViewModel()
        {
            VisibilityUpdate = "Visible";
            UpdateInformation = GLUpdater.Model.GetResult();
            UpdateApplicationButton = new RelayCommand(OnUpdateApplicationButtonExecuted, CanUpdateApplicationButtonExecute);
        }
    }
}
