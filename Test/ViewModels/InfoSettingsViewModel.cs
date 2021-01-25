using System.Diagnostics;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.ViewModels.Base;

namespace Test.ViewModels
{
    internal class InfoSettingsViewModel : ViewModel
    {
        public ICommand HelpMembers { get; }

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


        public InfoSettingsViewModel()
        {
            HelpMembers = new RelayCommand(OnHelpMembersCommandExecuted, CanHelpMembersCommandExecute);
        }
    }
}
