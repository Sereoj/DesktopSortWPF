using System.Windows.Controls;
using Test.Models;

namespace Test.Views.Controls
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