using System;

namespace Test.Models
{
    [Serializable]
    internal class Setting
    {
        /*
         * Catalog - название новой папки для каждого объекта
         * Extension - Расширение объектов для формирование Каталога.
         */

        public string ID { get; set; }
        public bool IsChecked { get; set; }

        public string Catalog { get; set; }
        public string Extension { get; set; }
    }
}