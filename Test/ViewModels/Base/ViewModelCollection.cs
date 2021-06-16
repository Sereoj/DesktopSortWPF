using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.ViewModels.Base
{
    public class ViewModelCollection
    {
        public MessengerVM MessengerVM { get; set; }
        public ImagerVM ImagerVM { get; set; }
        public FileManagerVM FileManagerVM { get; set; }
        public SettingsWindowViewModel SettingsWindowViewModel {
            get; set;
        }
        public UpdateControlViewModel UpdateControlViewModel{ get; set; }

        public IconChangerVM IconChangerVM { get; set; }
        public ViewModelCollection()
        {
            MessengerVM = new MessengerVM();
            ImagerVM = new ImagerVM();
            IconChangerVM = new IconChangerVM();
            FileManagerVM = new FileManagerVM(MessengerVM, IconChangerVM);
        }
    }
}
