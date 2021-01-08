namespace Test.Models.Settings
{
    public class AdvancedSettings
    {
        public AdvancedConfig AdvancedConfig { get; set; }

        public AdvancedSettings() => AdvancedConfig = new AdvancedConfig();
    }
}