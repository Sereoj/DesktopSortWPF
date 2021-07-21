using System.IO;

namespace DesktopSort.UI.Models.Helpers
{
    static public class StringHelperFile
    {
        static public bool IsFile(this string path)
        {
            return !string.IsNullOrEmpty(path) && File.Exists(path);
        }
    }
}