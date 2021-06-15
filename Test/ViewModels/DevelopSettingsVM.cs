using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using Test.ViewModels.Base;

namespace Test.ViewModels
{
    public class DevelopSettingsVM : ViewModel
    {
        public ViewModelCollection ListVM
        {
            get;
        }
        public ModelCollection ModelCollection
        {
            get;
        }
        public DevelopSettingsVM()
        {
        }
        public DevelopSettingsVM(ViewModelCollection listVM, ModelCollection modelCollection)
        {
            ListVM = listVM;
            ModelCollection = modelCollection;
        }
    }
}
