namespace DesktopSort.UI.Models.Settings
{
    public class AdvancedSettings
    {
        public AdvancedSettings()
        {
            AdvancedConfig = new AdvancedConfig();
        }

        public AdvancedConfig AdvancedConfig
        {
            get;
            set;
        }
    }
}