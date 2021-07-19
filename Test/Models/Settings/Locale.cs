using System.Globalization;

namespace Test.Models.Settings
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
            string lang = null;
            switch (langCode)
            {
                case Translation.Russia:
                    lang = "ru-RU";
                    break;
                case Translation.English:
                    lang = "en-US";
                    break;
                default:
                    lang = "en-US";
                    break;
            }

            WPFLocalizeExtension.Engine.LocalizeDictionary.Instance.Culture = new CultureInfo(lang);
        }
    }
}