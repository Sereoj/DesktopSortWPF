using System;
using System.Configuration;
using System.Xml.Serialization;

namespace Test.Models
{

    [Serializable]
    [XmlRoot("Config")]
    internal class Setting
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
    [XmlRoot("Background")]
    internal class Background
    {
        /*
         * IsBackground - Использовать ли изображения в качестве фона.
         * Path - Путь до картинки
         */
        public bool IsBackground { get; set; }
        public string Path { get; set; }
    }

    [Serializable]
    [XmlRoot("Settings")]
    internal class SettingsConfin
    {
    }
}