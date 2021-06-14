using System.Windows;
using System.Windows.Input;
using Test.ViewModels;

namespace Test
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = new MainWindowViewModel();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ( e.LeftButton != MouseButtonState.Pressed )
                return;
            DragMove();
        }
    }
}