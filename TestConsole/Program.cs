using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Test.Models.Settings;

namespace TestConsole
{
    internal class Program
    {
        /// <summary>
        /// Пример для реализации настроек xml
        /// </summary>
        /// <param name="args"></param>

        private static void Main(string[] args)
        {
            


            ItemBag item = new ItemBag()
            {
                Bag = new Bag
                {
                    BagConfig = new BagConfig { AncientRate = 1, ZenDrop = 1, ExcRate = 1, ItemRate = 1, Name = "ss", SocketRate = 1 }
                },
                Default = new Default
                {
                    DefaultConfig = new DefaultConfig { Addopt = 1, Anc = 1, Category = 1, Exc = 1, ID = 1, Luck = 1, MaxLevel = 1, MinLevel = 1, Skill = 1, Sock = 1 }
                },
                Items = new List<Item>()
                {
                    { new Item { Addopt = 1, Anc = 1, Category = 1, Exc = 1, ID = 1, Luck = 1, MaxLevel = 1, MinLevel = 1, Skill = 1, Sock = 1 }}
                }
            };

                //string serialized = item.XmlSerialize();

            //Console.WriteLine(serialized);
            Console.ReadLine();
        }
    }
}
