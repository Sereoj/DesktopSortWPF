using System;
using System.Collections;
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
        public bool Logger { get; set; }
    }
}