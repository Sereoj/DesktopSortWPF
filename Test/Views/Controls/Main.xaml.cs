using System.Windows.Controls;
using Test.ViewModels;

namespace Test.Views.Controls
{
    /// <summary>
    ///     Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        public Main()
        {
            InitializeComponent();
            //DataContext = new MainViewModel();
        }
    }
}