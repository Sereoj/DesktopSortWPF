using System.Xml.Serialization;

namespace DesktopSort.UI.Models.Settings
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
        [XmlAttribute]
        public bool OnlyOldFiles{ get; set; }
        [XmlAttribute]
        public bool OnlyNewFiles{ get; set; }
        [XmlAttribute]
        public bool OnlyDuplicateFiles{ get; set; }
    }
}