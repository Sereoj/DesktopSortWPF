using System.Windows.Controls;
using Test.Models;

namespace Test.Views.Controls
{
    /// <summary>
    /// Логика взаимодействия для UpdateContol.xaml
    /// </summary>
    public partial class UpdateControl : UserControl
    {
        public ModelCollection ModelCollection
        {
            get;
        }
        public UpdateControl()
        {
        }

        public UpdateControl(ModelCollection modelCollection)
        {
            InitializeComponent();
            ModelCollection = modelCollection;
        }
    }
}
