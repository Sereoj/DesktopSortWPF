using System.Xml.Serialization;

namespace TestConsole
{
    public class BagConfig
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public int ZenDrop { get; set; }

        [XmlAttribute]
        public int ItemRate { get; set; }

        [XmlAttribute]
        public int ExcRate { get; set; }

        [XmlAttribute]
        public int AncientRate { get; set; }

        [XmlAttribute]
        public int SocketRate { get; set; }
    }
}