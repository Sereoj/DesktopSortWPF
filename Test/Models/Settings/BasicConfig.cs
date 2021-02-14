using System.Xml.Serialization;

namespace Test.Models.Settings
{
    public class BasicConfig
    {
        /*
        Индивидуальная настройка каждого объекта, который имеет id и "включен" ли он. 
        */
        [XmlAttribute]
        public string ID { get; set; }
        [XmlAttribute]
        public bool IsChecked { get; set; }

        [XmlAttribute]
        public string Catalog { get; set; }
        [XmlAttribute]
        public string Extension { get; set; }
        [XmlAttribute]
        public string IconPath { get; set; }
    }
}