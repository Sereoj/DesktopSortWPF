using System.Diagnostics;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.Models;
using Test.ViewModels.Base;

namespace Test.ViewModels
{
    public class InfoSettingsViewModel : ViewModel, IApplicationContentView
    {
        private bool _isLoading;
        private ViewModelCollection listVM;
        private ModelCollection modelCollection;

        public ICommand HelpMembers { get; }

        public string Name => "Настройки // О программе";

        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }

        private bool CanHelpMembersCommandExecute(object p)
        {
            return true;
        }

        private void OnHelpMembersCommandExecuted(object p)
        {
            switch(p)
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

        public void Init()
        {
            throw new System.NotImplementedException();
        }

        public InfoSettingsViewModel()
        {
        }

        public InfoSettingsViewModel(ViewModelCollection listVM, ModelCollection modelCollection)
        {
            this.listVM = listVM;
            this.modelCollection = modelCollection;

            HelpMembers = new RelayCommand(OnHelpMembersCommandExecuted, CanHelpMembersCommandExecute);
        }
    }
}
