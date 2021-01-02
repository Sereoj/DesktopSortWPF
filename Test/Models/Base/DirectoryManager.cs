using System.IO;


namespace Test.Models.Base
{
    internal class DirectoryManager
    {
        /*
         * Класс для псевдо-работы с папками.
         * Базовый класс
         * Такие легкие действия, как создать, проверить, удалить. Без какой-то либо проверки!
         */

        /*
         * Создание папки
         * 
         */
        public void CreateDirectory(string Path)
        {
            Directory.CreateDirectory(Path);
        }
        /*
         * Проверка директории
         */
        public bool IsPathExists(string Path)
        {
            return !Directory.Exists(Path) ? true : false;
        }

        public void DeleteDirectory(string Path)
        {
            Directory.Delete(Path);
        }
    }
}
