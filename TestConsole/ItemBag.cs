using System.Collections.Generic;
using System.Xml.Serialization;

namespace TestConsole
{
    [XmlRoot]
    public class ItemBag
    {
        public Bag Bag { get; set; }

        public Default Default { get; set; }

        [XmlArray("Items")]
        [XmlArrayItem("Item")]
        public List<Item> Items { get; set; }

        public ItemBag()
        {
            Bag = new Bag();
            Default = new Default();
            Items = new List<Item>();
        }
    }
}