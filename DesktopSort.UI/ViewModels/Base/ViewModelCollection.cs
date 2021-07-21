namespace DesktopSort.UI.ViewModels.Base
{
    public class ViewModelCollection
    {
        public ViewModelCollection()
        {
            MessengerVM = new MessengerVM();
            ImagerVM = new ImagerVM();
            IconChangerVM = new IconChangerVM();
            FileManagerVM = new FileManagerVM(MessengerVM, IconChangerVM);
            UpdaterVM = new UpdaterVM(MessengerVM);
        }

        public MessengerVM MessengerVM
        {
            get;
            set;
        }

        public ImagerVM ImagerVM
        {
            get;
            set;
        }

        public FileManagerVM FileManagerVM
        {
            get;
            set;
        }

        public UpdaterVM UpdaterVM
        {
            get;
            set;
        }

        public SettingsWindowViewModel SettingsWindowViewModel
        {
            get;
            set;
        }

        public UpdateControlViewModel UpdateControlViewModel
        {
            get;
            set;
        }

        public IconChangerVM IconChangerVM
        {
            get;
            set;
        }
    }
}