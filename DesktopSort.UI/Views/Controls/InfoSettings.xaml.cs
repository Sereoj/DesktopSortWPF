using System.Windows.Controls;
using DesktopSort.UI.Models;

namespace DesktopSort.UI.Views.Controls
{
    /// <summary>
    ///     Логика взаимодействия для InfoSettings.xaml
    /// </summary>
    public partial class InfoSettings : UserControl
    {

        public ModelCollection ModelCollection
        {
            get;
        }
        public InfoSettings()
        {
        }

        public InfoSettings(ModelCollection modelCollection)
        {
            InitializeComponent();
            ModelCollection = modelCollection;
        }

    }
}