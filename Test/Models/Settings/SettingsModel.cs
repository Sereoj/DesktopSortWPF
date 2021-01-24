using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Test.Models.Settings
{
    [XmlRoot("Settings")]
    [Serializable]
    public class SettingsModel
    {
        private static SettingsModel _instance;
        public AdvancedSettings Advanced { get; set; }

        [XmlArray("Items")]
        [XmlArrayItem("Item")]
        public List<BasicConfig> Items { get; set; }


        public static SettingsModel Instance
        {
            get => _instance;
            set => _instance = value;
        }

        public static void CreateInstance()
        {
            //Instance = new SettingsModel() {Advanced = new AdvancedSettings(), Items = new List<BasicConfig>()};
            Instance = Load<SettingsModel>("data.xml");
        }

        public static void Update(SettingsModel model)
        {
            Save(model);
            CreateInstance();
        }

        public static void Default()
        {
            var item = new SettingsModel
            {
                Advanced = new AdvancedSettings
                {
                    AdvancedConfig = new AdvancedConfig { Update = true, Background = "standard", IsBackground = true, Theme = "light", Logger = false }
                },
                Items = new List<BasicConfig>
                {
                    new BasicConfig{ID = "1", IsChecked = false, Catalog = "Word", Extension = ".docx,.dotx,.doc,.dot"},
                    new BasicConfig{ID = "2", IsChecked = false, Catalog = "Excel", Extension = ".xlsx,.xlsm,.xltx,.xltm,.xlam,.xls,.xlt,.xla"},
                    new BasicConfig{ID = "3", IsChecked = false, Catalog = "Access", Extension = ".accdb,.mdb"},
                    new BasicConfig{ID = "4", IsChecked = false, Catalog = "Project", Extension = ".mpp"},
                    new BasicConfig{ID = "5", IsChecked = false, Catalog = "PowerPoint", Extension = ""},
                    new BasicConfig{ID = "6", IsChecked = false, Catalog = "OneNote", Extension = ""},
                    new BasicConfig{ID = "7", IsChecked = false, Catalog = "Text", Extension = ".txt,.log"},
                    new BasicConfig{ID = "8", IsChecked = false, Catalog = "Image", Extension = ".bmp,.tif,.jpg,.gif,.png,.ico"},
                    new BasicConfig{ID = "9", IsChecked = false, Catalog = "Media", Extension = ".avi,.mp4,.mpeg,.wmv,.mp3"},
                    new BasicConfig{ID = "10", IsChecked = false, Catalog = "Archive", Extension = ".rar,.zip,.7z"},
                    new BasicConfig{ID = "11", IsChecked = false, Catalog = "eBook", Extension = ".fb2,.epub,.mobi,.pdf,.djvu"},
                    new BasicConfig{ID = "12", IsChecked = false, Catalog = "Other\\Template1", Extension = ""},
                    new BasicConfig{ID = "13", IsChecked = false, Catalog = "Other\\Template2", Extension = ""},
                    new BasicConfig{ID = "14", IsChecked = false, Catalog = "Other\\Template3", Extension = ""},
                    new BasicConfig{ID = "15", IsChecked = false, Catalog = "Other\\Template4", Extension = ""},
                    new BasicConfig{ID = "16", IsChecked = false, Catalog = "Other\\Template5", Extension = ""},
                    new BasicConfig{ID = "17", IsChecked = false, Catalog = "Other\\Template6", Extension = ""},
                    new BasicConfig{ID = "18", IsChecked = false, Catalog = "Other\\Template7", Extension = ""},
                    new BasicConfig{ID = "19", IsChecked = false, Catalog = "Other\\Template8", Extension = ""},
                    new BasicConfig{ID = "20", IsChecked = false, Catalog = "Other\\Template9", Extension = ""},
                    new BasicConfig{ID = "21", IsChecked = false, Catalog = "Other\\Template10", Extension = ""}
                }
            };

            Save(item);
        }

        private static void Save<T>(T subject)
        {
            var xml = new XmlSerializer(typeof(SettingsModel));
            using var file = new FileStream("data.xml", FileMode.Create);
            xml.Serialize(file, subject);
        }

        private static T Load<T>(string FileName)
        {
            if (!File.Exists("data.xml"))
                Default();
            var xml = new XmlSerializer(typeof(SettingsModel));
            using var file = new FileStream(FileName, FileMode.Open);
            return (T)xml.Deserialize(file);
        }
    }
}