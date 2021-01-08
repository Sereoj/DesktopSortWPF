using System.Xml.Serialization;

namespace TestConsole
{
    public class DefaultConfig
    {
        [XmlAttribute("cat")]
        public int Category { get; set; }

        [XmlAttribute("id")]
        public int ID { get; set; }

        [XmlAttribute("minlv")]
        public int MinLevel { get; set; }

        [XmlAttribute("maxlv")]
        public int MaxLevel { get; set; }

        [XmlAttribute("skill")]
        public int Skill { get; set; }

        [XmlAttribute("luck")]
        public int Luck { get; set; }

        [XmlAttribute("addopt")]
        public int Addopt { get; set; }

        [XmlAttribute("exc")]
        public int Exc { get; set; }

        [XmlAttribute("anc")]
        public int Anc { get; set; }

        [XmlAttribute("sock")]
        public int Sock { get; set; }
    }
}