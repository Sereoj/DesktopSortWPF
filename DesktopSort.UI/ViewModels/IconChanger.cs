using System.IO;
using System.Text;
using DesktopSort.UI.Models.FileManagerModel;
using DesktopSort.UI.ViewModels.Base;

namespace DesktopSort.UI.ViewModels
{
    public class IconChangerVM : ViewModel
    {
        public void FolderIcon(string targetFolderPath, string iconFilePath)
        {
            var iniPath = Path.Combine(targetFolderPath, "desktop.ini");
            if (iniPath.IsPathExists())
                File.SetAttributes(
                    iniPath,
                    File.GetAttributes(iniPath) &
                    ~(FileAttributes.Hidden | FileAttributes.System));

            var iniContents = new StringBuilder()
                .AppendLine("[.ShellClassInfo]")
                .AppendLine($"IconResource={iconFilePath},0")
                .AppendLine($"IconFile={iconFilePath}")
                .AppendLine("IconIndex=0")
                .ToString();
            File.WriteAllText(iniPath, iniContents);

            File.SetAttributes(
                iniPath,
                File.GetAttributes(iniPath) | FileAttributes.Hidden | FileAttributes.System);

            File.SetAttributes(
                targetFolderPath,
                File.GetAttributes(targetFolderPath) | FileAttributes.System);
        }
    }
}