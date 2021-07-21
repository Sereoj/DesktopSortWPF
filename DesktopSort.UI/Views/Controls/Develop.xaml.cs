using System.Windows.Controls;
using DesktopSort.UI.Models;

namespace DesktopSort.UI.Views.Controls
{
    /// <summary>
    ///     Логика взаимодействия для Develop.xaml
    /// </summary>
    public partial class Develop : UserControl
    {
        public Develop()
        {
        }

        public Develop(ModelCollection modelCollection)
        {
            InitializeComponent();
            ModelCollection = modelCollection;
        }

        public ModelCollection ModelCollection
        {
            get;
        }
    }
}