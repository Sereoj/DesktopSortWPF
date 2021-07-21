using System;
using System.Diagnostics;
using System.Windows.Input;
using DesktopSort.UI.Infrastucture.Commands;
using DesktopSort.UI.Models;
using DesktopSort.UI.ViewModels.Base;

namespace DesktopSort.UI.ViewModels
{
    public class InfoSettingsViewModel : ViewModel, IApplicationContentView
    {
        private ICommand _HelpMembers;

        private bool _isLoading;

        public InfoSettingsViewModel()
        {
        }

        public InfoSettingsViewModel(ViewModelCollection listVM, ModelCollection modelCollection)
        {
            ListVm = listVM;
            ModelCollection = modelCollection;
        }

        public ViewModelCollection ListVm
        {
            get;
            set;
        }

        public ModelCollection ModelCollection
        {
            get;
            set;
        }

        public ICommand HelpMembers =>
            _HelpMembers ??= new RelayCommand(OnHelpMembersCommandExecuted, CanHelpMembersCommandExecute);

        public string Name => "InfoSettingsTitle";

        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        private bool CanHelpMembersCommandExecute(object p)
        {
            return true;
        }

        private void OnHelpMembersCommandExecuted(object p)
        {
            switch (p)
            {
                case "member1":
                    Process.Start("https://github.com/sergiostranges");
                    break;
                case "member2":
                    Process.Start("https://github.com/Mer-hi");
                    break;
                case "site":
                    Process.Start("https://w2me.ru");
                    break;
                case "github":
                    Process.Start("https://github.com/sereoj");
                    break;
            }
        }
    }
}