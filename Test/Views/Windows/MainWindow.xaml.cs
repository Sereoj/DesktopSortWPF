using System.Windows;
using System.Windows.Input;
using Test.Models;
using Test.Models.Settings;
using Test.ViewModels;
using Test.ViewModels.Base;

namespace Test
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var listVm = new ViewModelCollection();
            var modelCollection = new ModelCollection();
            Locale.Set(modelCollection.SettingsModel.Advanced.AdvancedConfig.Language);
            var mainWindowViewModel = new MainWindowViewModel(listVm, modelCollection);
            
            this.DataContext = mainWindowViewModel;
            InitializeComponent();
            
        }
        

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ( e.LeftButton != MouseButtonState.Pressed )
                return;
            DragMove();
        }
    }
}