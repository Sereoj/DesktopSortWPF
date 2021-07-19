using System.Windows;
using System.Windows.Input;
using Test.Models;
using Test.Models.Settings;
using Test.ViewModels;
using Test.ViewModels.Base;
using WPFLocalizeExtension.Deprecated.Extensions;
using WPFLocalizeExtension.Extensions;

namespace Test
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty FormatSegment1Property = DependencyProperty.RegisterAttached(
            "FormatSegment1", typeof(string), typeof(LocExtension), new PropertyMetadata(default(string)));

        public static void SetFormatSegment1(DependencyObject element, string value)
        {
            element.SetValue(FormatSegment1Property, value);
        }

        public static string GetFormatSegment1(DependencyObject element)
        {
            return (string)element.GetValue(FormatSegment1Property);
        }
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