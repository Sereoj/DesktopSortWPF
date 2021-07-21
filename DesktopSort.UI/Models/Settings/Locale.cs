using System.Globalization;

namespace DesktopSort.UI.Models.Settings
{
    public class Locale
    {
        public enum Translation
        {
            English,
            Russia
        }

        public static void Set(Translation langCode)
        {
            string lang = langCode switch
            {
                Translation.Russia => "ru-RU",
                Translation.English => "en-US",
                _ => "en-US"
            };

            WPFLocalizeExtension.Engine.LocalizeDictionary.Instance.Culture = new CultureInfo(lang);
        }
    }
}