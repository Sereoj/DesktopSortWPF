using System.IO;

namespace DesktopSort.UI.Models.Helpers
{
    public static class StringHelperFile
    {
        public static bool IsFile(this string path)
        {
            return !string.IsNullOrEmpty(path) && File.Exists(path);
        }
    }
}
