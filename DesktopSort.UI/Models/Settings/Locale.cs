using System.Globalization;
using WPFLocalizeExtension.Engine;

namespace DesktopSort.UI.Models.Settings
{
    public class Locale
    {
        public enum Translation
        {
            English,
            Russia
        }

        static public void Set(Translation langCode)
        {
            var lang = langCode switch
            {
                Translation.Russia => "ru-RU",
                Translation.English => "en-US",
                _ => "en-US"
            };

            LocalizeDictionary.Instance.Culture = new CultureInfo(lang);
        }
    }
}