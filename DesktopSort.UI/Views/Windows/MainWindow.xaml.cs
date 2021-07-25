using System.Threading;
using System.Windows;
using System.Windows.Input;
using DesktopSort.UI.Models;
using DesktopSort.UI.Models.Settings;
using DesktopSort.UI.ViewModels;
using DesktopSort.UI.ViewModels.Base;

namespace DesktopSort.UI.Views.Windows
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

            listVm.UpdaterVM.PropertyChanged += UpdaterVM_PropertyChanged;

            DataContext = mainWindowViewModel;
            InitializeComponent();
        }

        private void UpdaterVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "OnCloseProgram")
            {
                Dispatcher.Invoke(() => {
                    Close();
                });
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;
            DragMove();
        }
    }
}