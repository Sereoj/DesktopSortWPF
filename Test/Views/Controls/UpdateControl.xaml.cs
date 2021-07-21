using System.Windows.Controls;
using DesktopSort.UI.Models;

namespace DesktopSort.UI.Views.Controls
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
