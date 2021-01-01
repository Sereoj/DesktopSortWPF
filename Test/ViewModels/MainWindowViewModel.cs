using System;
using System.Windows;
using System.Windows.Input;
using Test.Infrastucture.Commands;
using Test.ViewModels.Base;

namespace Test.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Values
        private string _Title = "Desktop Sort 0.1";

        private string _PathImageBackground = "/Images/Background.bmp";

        //private string _ColorPrimary = "#FF2E1795";
        //private string _ColorSecondary = "#FF150851";

        private string _TextBoxPath = "C://Windows";
        private string _TextBoxPath1 = "C://Windows/Backup";


        public string Title { get => _Title; set => Set(ref _Title, value); }
        public string PathImageBackground { get => _PathImageBackground; set => Set(ref _PathImageBackground, value); }

        public string TextBoxPath { get => _TextBoxPath; set => Set(ref _TextBoxPath, value); }
        public string TextBoxPath1 { get => _TextBoxPath1; set => Set(ref _TextBoxPath1, value); }
        #endregion

        #region Commands

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion
        #endregion

        public MainWindowViewModel()
        {
            #region Commands
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            #endregion
        }
    }
}
