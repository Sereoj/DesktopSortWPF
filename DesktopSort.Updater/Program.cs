using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopSort.Updater.MarkupExtension;

namespace DesktopSort.Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (File.Exists("DesktopSort.UI.exe")) File.Delete("DesktopSort.UI.exe");

                if (File.Exists("DesktopSort.exe")) File.Delete("DesktopSort.exe");

                var archiveFileName = Path.Combine(Environment.CurrentDirectory,"update.zip");
                using (ZipArchive zip = ZipFile.OpenRead(archiveFileName))
                {
                    zip.ExtractToDirectory(Environment.CurrentDirectory, true);
                }
                File.Delete(archiveFileName);

                Process.Start("DesktopSort.exe");
            }
            catch (IOException exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
