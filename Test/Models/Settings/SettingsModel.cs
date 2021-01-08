using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Test.Models.Settings
{
    [XmlRoot ("Settings")]
    [Serializable]
    public class SettingsModel
    {
        public AdvancedSettings Advanced { get; set; }

        [XmlArray("Items")]
        [XmlArrayItem("Item")]
        public List<BasicConfig> Items { get; set; }


        public static SettingsModel Instance { get; private set; }
        public static SettingsModel Settings { get; private set; }

        public static void CreateInstance()
        {
            //Instance = new SettingsModel() {Advanced = new AdvancedSettings(), Items = new List<BasicConfig>()};
            Instance = Load<SettingsModel>("data.xml");
        }


        public static void Default()
        {
            var item = new SettingsModel
            {
                Advanced = new AdvancedSettings
                {
                    AdvancedConfig = new AdvancedConfig{ Update = true, Background = "/Test;component/Images/Background.bmp", IsBackground = true, Logger = false}
                },
                Items = new List<BasicConfig>
                {
                    new BasicConfig{ID = "CheckWord", IsChecked = true, Catalog = "Word", Extension = ".docx,.dotx,.doc,.dot"},
                    new BasicConfig{ID = "CheckExcel", IsChecked = true, Catalog = "Excel", Extension = ".xlsx,.xlsm,.xltx,.xltm,.xlam,.xls,.xlt,.xla"},
                    new BasicConfig{ID = "CheckAccess", IsChecked = true, Catalog = "Access", Extension = ".accdb,.mdb"},
                    new BasicConfig{ID = "CheckImage", IsChecked = true, Catalog = "Image", Extension = ".bmp,.tif,.jpg,.gif,.png,.ico"},
                    new BasicConfig{ID = "CheckTextDoc", IsChecked = true, Catalog = "Text", Extension = ".txt,.log"},
                    new BasicConfig{ID = "CheckProject", IsChecked = false, Catalog = "Project", Extension = ".mpp"},
                    new BasicConfig{ID = "CheckArchive", IsChecked = false, Catalog = "Archive", Extension = ".rar,.zip,.7z"},
                    new BasicConfig{ID = "CheckPDF", IsChecked = false, Catalog = "eBook", Extension = ".fb2,.epub,.mobi,.pdf,.djvu"},
                    new BasicConfig{ID = "CheckMedia", IsChecked = false, Catalog = "Media", Extension = ".avi,.mp4,.mpeg,.wmv,.mp3"},
                    new BasicConfig{ID = "Template1", IsChecked = false, Catalog = "Other\\Template1", Extension = ""},
                    new BasicConfig{ID = "Template2", IsChecked = false, Catalog = "Other\\Template2", Extension = ""},
                    new BasicConfig{ID = "Template3", IsChecked = false, Catalog = "Other\\Template3", Extension = ""},
                    new BasicConfig{ID = "Template4", IsChecked = false, Catalog = "Other\\Template4", Extension = ""},
                    new BasicConfig{ID = "Template5", IsChecked = false, Catalog = "Other\\Template5", Extension = ""},
                    new BasicConfig{ID = "Template6", IsChecked = false, Catalog = "Other\\Template6", Extension = ""},
                    new BasicConfig{ID = "Template7", IsChecked = false, Catalog = "Other\\Template7", Extension = ""},
                    new BasicConfig{ID = "Template8", IsChecked = false, Catalog = "Other\\Template8", Extension = ""},
                    new BasicConfig{ID = "Template9", IsChecked = false, Catalog = "Other\\Template9", Extension = ""},
                    new BasicConfig{ID = "Template10", IsChecked = false, Catalog = "Other\\Template10", Extension = ""}
                }
            };

            Save(item);
        }

        public static void Save<T>(T subject)
        {
            var xml = new XmlSerializer(typeof(SettingsModel));
            using var file = new FileStream("data.xml", FileMode.Create);
            xml.Serialize(file, subject);
        }

        public static T Load<T>(string FileName)
        {
            if (!File.Exists("data.xml"))
                Default();
            var xml = new XmlSerializer(typeof(SettingsModel));
            using var file = new FileStream(FileName, FileMode.Open);
            return (T)xml.Deserialize(file);
        }
    }
}