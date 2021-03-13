using System.Xml.Serialization;

namespace Test.Models.Settings
{

    public class AdvancedConfig
    {
        [XmlAttribute]
        public string Background { get; set; }
        [XmlAttribute]
        public bool IsBackground { get; set; }
        [XmlAttribute]
        public bool Update { get; set; }
        [XmlAttribute]
        public string Theme { get; set; }

        [XmlAttribute]
        public bool DeleteDefaultDirectory { get; set; }

        [XmlAttribute]
        public string InputPath { get; set; }
        [XmlAttribute]
        public string OutputPath { get; set; }
    }
}