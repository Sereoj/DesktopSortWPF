using System.Diagnostics;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.ViewModels.Base;

namespace Test.ViewModels
{
    internal class InfoSettingsViewModel : ViewModel
    {
        public ICommand HelpMember1 { get; }

        private bool CanHelpMember1CommandExecute(object p)
        {
            return true;
        }

        private void OnHelpMember1CommandExecuted(object p)
        {
            Process.Start("https://github.com/sergiostranges");
        }

        public ICommand HelpMember2 { get; }

        private bool CanHelpMember2CommandExecute(object p)
        {
            return true;
        }

        private void OnHelpMember2CommandExecuted(object p)
        {
            Process.Start("https://github.com/Mer-hi");
        }
        public InfoSettingsViewModel()
        {
            HelpMember1 = new RelayCommand(OnHelpMember1CommandExecuted, CanHelpMember1CommandExecute);
            HelpMember2 = new RelayCommand(OnHelpMember2CommandExecuted, CanHelpMember2CommandExecute);
        }
    }
}
