using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models.Settings
{
    internal class Background
    {
        /*
         * IsBackground - Использовать ли изображения в качестве фона.
         * Path - Путь до картинки
         */
        public bool IsBackground { get; set; }
        public string Path { get; set; }
    }
}
