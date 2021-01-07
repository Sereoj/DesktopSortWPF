using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Test.Models.Settings
{
    [Serializable]
    [XmlRoot("Advanced")]
    public class Alternative
    {
        public string Background { get; set; }
        public bool IsBackground { get; set; }

        public bool Update { get; set; }
        public bool Logger { get; set; }

    }

    [Serializable]
    [XmlRoot("Basic")]
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
    public class SettingsModel : Alternative
    {
        [NonSerialized] public string FileName;

        /// <summary>
        ///     Основные настройки
        /// </summary>
        public static List<Setting> Setting { get; set; }


        public static SettingsModel Instance { get; private set; }

        public static void CreateInstance()
        {
            Instance = new SettingsModel();
            Setting = new List<Setting>();
        }

        public static void Default()
        {
            SettingsModel addSettings = new SettingsModel(  );

                Setting.Add(new Setting(){ IsChecked = true});

            //Save(Settings);
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
            return (T) xml.Deserialize(file);
        }
    }
}