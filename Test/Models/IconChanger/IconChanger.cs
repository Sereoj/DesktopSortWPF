using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Test.ViewModels.Base;

namespace Test.Models.IconChanger
{

    internal class IconChanger : ViewModel
    {
        public static void FolderIcon( string targetFolderPath, string iconFilePath )
        {
            var iniPath = Path.Combine(targetFolderPath, "desktop.ini");
            if ( File.Exists(iniPath) )
            {
                File.SetAttributes(
                   iniPath,
                   File.GetAttributes(iniPath) &
                   ~(FileAttributes.Hidden | FileAttributes.System));
            }

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
