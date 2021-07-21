using System.IO;

namespace DesktopSort.UI.Models.FileManagerModel
{
    static public class StringHelperDirectory
    {
        static public void CreateDirectory(this string Path)
        {
            Directory.CreateDirectory(Path);
        }

        /// <summary>Проверка директории</summary>
        /// <param name="Path"></param>
        static public bool IsPathExists(this string Path)
        {
            return !Directory.Exists(Path);
        }

        /// <summary>Удаление директории</summary>
        /// <param name="Path"></param>
        static public void DeleteDirectory(this string Path)
        {
            Directory.Delete(Path, true);
        }
    }
}