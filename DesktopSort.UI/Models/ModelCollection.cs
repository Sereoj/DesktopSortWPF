using DesktopSort.UI.Models.Settings;
using DesktopSort.UI.ViewModels.Base;

namespace DesktopSort.UI.Models
{
    public class ModelCollection : ViewModel
    {
        private bool isDefaultSettings;

        public ModelCollection()
        {
            var settings = new SettingsModel();
            SettingsModel = settings.Load<SettingsModel>("data.xml");

            ThemeModel = new Theme.Theme();
            VersionModel = new Version();

            IsDefaultSettings = false;
        }

        public bool IsDefaultSettings
        {
            get => isDefaultSettings;
            set => Set(ref isDefaultSettings, value);
        }

        public SettingsModel SettingsModel
        {
            get;
            set;
        }

        public Theme.Theme ThemeModel
        {
            get;
            set;
        }

        public Version VersionModel
        {
            get;
            set;
        }
    }
}