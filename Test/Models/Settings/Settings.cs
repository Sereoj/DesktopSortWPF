using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Test.Models.Settings
{
    public class Alternative
    {
    }

    [Serializable]
    public class Setting
    {
        /*
        Индувидуальная настройка каждого объекта, который имеет id и "включен" ли он. 
             
        */
        public string ID { get; set; }
        public bool IsChecked { get; set; }


        public string Catalog { get; set; }
        public string Extension { get; set; }
    }


    [Serializable]
    [XmlRoot("Settings")]
    public class SettingsModel
    {
        [NonSerialized] public string FileName;

        /// <summary>
        ///     Основные настройки
        /// </summary>
        public List<Setting> Setting { get; set; }

        /// <summary>
        ///     Альтернативные настройки скрытые от пользователя.
        /// </summary>
        public List<Alternative> Alternative { get; set; }

        /// <summary>
        ///     Проверять на обновления
        /// </summary>
        public bool Update { get; set; }

        /// <summary>
        ///     Вести ли логи.
        /// </summary>
        public bool Logger { get; set; }

        public bool Background { get; set; }

        public static SettingsModel Instance { get; private set; }

        public static void CreateInstance()
        {
            Instance = new SettingsModel();
        }

        public static void Default()
        {
            var Settings = new SettingsModel
            {
                Setting = new List<Setting>(),
                Alternative = new List<Alternative>()
            };

            Settings.Setting.Add(new Setting
            { ID = "CheckWord", IsChecked = true, Catalog = "Word", Extension = "*.docx,*.dotx,*.doc,*.dot" });
            Settings.Setting.Add(new Setting
            {
                ID = "CheckExcel",
                IsChecked = true,
                Catalog = "Excel",
                Extension = "*.xlsx,*.xlsm,*.xltx,*.xltm,*.xlam,*.xls,*.xlt,*.xla"
            });
            Settings.Setting.Add(new Setting
            { ID = "CheckAccess", IsChecked = true, Catalog = "Access", Extension = "*.accdb,*.mdb" });
            Settings.Setting.Add(new Setting
            {
                ID = "CheckImage",
                IsChecked = true,
                Catalog = "Image",
                Extension = "*.bmp,*.tif,*.jpg,*.gif,*.png,*.ico"
            });
            Settings.Setting.Add(new Setting
            { ID = "CheckTextDoc", IsChecked = true, Catalog = "Text", Extension = "*.txt,*.log" });
            Settings.Setting.Add(new Setting
            { ID = "CheckProject", IsChecked = false, Catalog = "Project", Extension = "*.mpp" });
            Settings.Setting.Add(new Setting
            { ID = "CheckArchive", IsChecked = false, Catalog = "Archive", Extension = "*.rar,*.zip,*.7z" });
            Settings.Setting.Add(new Setting
            {
                ID = "CheckPDF",
                IsChecked = false,
                Catalog = "eBook",
                Extension = "*.fb2,*.epub,*.mobi,*.pdf,*.djvu"
            });
            Settings.Setting.Add(new Setting
            {
                ID = "CheckMedia",
                IsChecked = false,
                Catalog = "Media",
                Extension = "*.avi,*.mp4,*.mpeg,*.wmv,*.mp3"
            });
            Settings.Setting.Add(new Setting
            { ID = "Template1", IsChecked = false, Catalog = "Other\\Template1", Extension = "" });
            Settings.Setting.Add(new Setting
            { ID = "Template2", IsChecked = false, Catalog = "Other\\Template2", Extension = "" });
            Settings.Setting.Add(new Setting
            { ID = "Template3", IsChecked = false, Catalog = "Other\\Template3", Extension = "" });
            Settings.Setting.Add(new Setting
            { ID = "Template4", IsChecked = false, Catalog = "Other\\Template4", Extension = "" });
            Settings.Setting.Add(new Setting
            { ID = "Template5", IsChecked = false, Catalog = "Other\\Template5", Extension = "" });
            Settings.Setting.Add(new Setting
            { ID = "Template6", IsChecked = false, Catalog = "Other\\Template6", Extension = "" });
            Settings.Setting.Add(new Setting
            { ID = "Template7", IsChecked = false, Catalog = "Other\\Template7", Extension = "" });
            Settings.Logger = true;
            Settings.Background = false;
            Settings.Update = false;

            Save(Settings);
        }

        public static void Save<T>(T subject)
        {
            var xml = new XmlSerializer(typeof(SettingsModel));
            using var file = new FileStream("data.xml", FileMode.Create);
            xml.Serialize(file, subject);
        }

        public static T Load<T>(string FileName)
        {
            if (!File.Exists("data.xml")) Default();
            var xml = new XmlSerializer(typeof(SettingsModel));
            using var file = new FileStream(FileName, FileMode.Open);
            return (T)xml.Deserialize(file);
        }
    }
}