using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models.Base;
using Test.ViewModels.Base;

namespace Test.Models
{
    internal class FileManager : DirectoryManager
    {
        public string DEFAULT_PATH { get; }
        public string OUTPUT_PATH { get; }
        public bool DeleteDefaultDirectory { get; }

        public enum FileMode : byte
        {
            Copy, // Копирование
            Move, // Перемещение
            Ignore // Игнорирование, использование в качестве тестирования функционала, игнорирования работы с файлами. 
        }

        public FileManager(string default_path, string path_output, bool deleteOutputDirectory = false)
        {
            if (string.IsNullOrWhiteSpace(default_path))
                throw new ArgumentNullException(default_path, "Default path is NULL.");
            if (string.IsNullOrWhiteSpace(path_output))
                throw new ArgumentNullException(path_output, "Output path is NULL.");


            if ((IsPathExists(default_path) && IsPathExists(path_output)) != true)
            {
                DEFAULT_PATH = default_path;
                OUTPUT_PATH = path_output;
                DeleteDefaultDirectory = deleteOutputDirectory;

                CreateDirectory(default_path); // Создаем, если пользователь указал неверный первичный путь
                CreateDirectory(path_output); // Создаем, если пользователь указал неверно вторичный путь.
            }
        }

        /*
        * Поиск файлов небезопасный, получение данных исключительно от программы.
        * Первая функция.
        */
        public bool SearchFiles(List<Setting> Files, FileMode modeFile)
        {
            foreach (var file in Files)
            {
                SearchFiles(file.Catalog, file.Extension, modeFile);
            }
            return false;
        }

        /*
        * Поиск файлов небезопасный, получение данных исключительно от программы.
        * Вторая функция.
        */
        public bool SearchFiles(string PathNewDirectory, string PatternExtension, FileMode modeFile)
        {
            var NewDirectory = OUTPUT_PATH + PathNewDirectory + @"\";
            if (IsPathExists(NewDirectory))
                CreateDirectory(NewDirectory);

            foreach (var GetAllFiles in Directory.GetFiles(DEFAULT_PATH, PatternExtension.ToLower(), SearchOption.TopDirectoryOnly))
            {
                try
                {
                    var GetExtension = Path.GetExtension(GetAllFiles);
                    if (GetExtension != null && PatternExtension.Contains(GetExtension.ToLower()) && GetExtension.Length > 0)
                    {
                        FileInfo InfoFile = new FileInfo(GetAllFiles);
                        if (!File.Exists(Path.Combine(NewDirectory, InfoFile.Name)))
                        {
                            switch (modeFile)
                            {
                                case FileMode.Copy:
                                    InfoFile.CopyTo(Path.Combine(NewDirectory, InfoFile.Name), true);
                                    break;
                                case FileMode.Move:
                                    InfoFile.MoveTo(Path.Combine(NewDirectory, InfoFile.Name));
                                    break;
                                case FileMode.Ignore:
                                    break;
                            }

                            return true;
                        }
                    }
                }
                catch (ArgumentException Message)
                {
                    Console.WriteLine(Message.Message);
                }
            }
            if (DeleteDefaultDirectory != false)
                DeleteDirectory(DEFAULT_PATH); // Удаление начальной папки

            return false;
        }
    }
}
