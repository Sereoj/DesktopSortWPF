using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Test.Models.Settings;
using Test.ViewModels.Base;

namespace Test.Models
{
    internal class FileManager : ViewModel
    {
        public enum FileMode : byte
        {
            Copy, // Копирование
            Move, // Перемещение
            Ignore // Игнорирование, использование в качестве тестирования функционала, игнорирования работы с файлами. 
        }

        private static FileManager _model;


        public string GetMessage { get; private set; }

        public static FileManager Model => _model ??= new FileManager();

        ~FileManager()
        {
        }

        #region закрытые поля

        private string INPUT_PATH;
        private string OUTPUT_PATH;
        private bool DeleteDefaultDirectory;
        private readonly int delay = 100;

        #endregion


        #region Публичные методы

        public void SetInput(string input)
        {
            if (Validate(input))
            {
                INPUT_PATH = input;
                SetMessage("Проверка: " + input);
            }
        }

        public void SetOutput(string output)
        {
            if (Validate(output))
            {
                OUTPUT_PATH = output;
                SetMessage("Проверка: " + output);
            }
        }

        public void SetDeleteDirectory(bool directory = false)
        {
            DeleteDefaultDirectory = directory;
        }

        /*
        * Поиск файлов небезопасный, получение данных исключительно от программы.
        * Первая функция.
        */
        public async Task SearchFiles(List<BasicConfig> Files, FileMode modeFile)
        {
            foreach (var file in Files)
            {
                await Task.Delay(delay);
                await SearchFilesAsyn(file.Catalog, file.Extension, modeFile);
            }

            SetMessage("Работа завершилась!");
        }

        /*
        * Поиск файлов небезопасный, получение данных исключительно от программы.
        * Вторая функция.
        */
        public async Task SearchFilesAsyn(string PathNewDirectory, string PatternExtension, FileMode modeFile)
        {
            var NewDirectory = Path.Combine(OUTPUT_PATH, PathNewDirectory);

            SetMessage("Начало выполнения: " + PathNewDirectory);
            await Task.Delay(delay);

            try
            {
                var files = GetFilesList(INPUT_PATH, PatternExtension);

                if (files != null && (IsPathExists(NewDirectory) && files.ToList().Count > 0))
                    CreateDirectory(NewDirectory);

                foreach (var fileSingle in files)
                {
                    var NewFile = Path.Combine(NewDirectory + "\\" + Path.GetFileName(fileSingle));

                    switch (modeFile)
                    {
                        case FileMode.Copy:
                            File.Copy(fileSingle, NewFile, true);
                            await Task.Delay(delay);
                            SetMessage(NewFile);
                            break;
                        case FileMode.Move:
                            File.Move(fileSingle, NewFile);
                            await Task.Delay(delay);
                            SetMessage(NewFile);
                            break;
                        case FileMode.Ignore:
                            File.Copy(fileSingle, NewFile, true);
                            await Task.Delay(delay);
                            SetMessage(NewFile);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (DeleteDefaultDirectory)
                DeleteDirectory(INPUT_PATH); // Удаление начальной папки

            SetMessage("Завершение выполнения:" + PathNewDirectory);
            await Task.Delay(delay);
            //SetMessage("Работа закончилась!");
        }

        #endregion

        #region Закрытые методы

        /// <summary>Создание директории</summary>
        /// <param name="Path"></param>
        private void CreateDirectory(string Path)
        {
            Directory.CreateDirectory(Path);
        }

        /// <summary>Проверка директории</summary>
        /// <param name="Path"></param>
        private bool IsPathExists(string Path) => !Directory.Exists(Path);

        /// <summary>Удаление директории</summary>
        /// <param name="Path"></param>
        private void DeleteDirectory(string Path)
        {
            Directory.Delete(Path);
        }

        /// <summary>Валидация пути</summary>
        /// <param name="Path"></param>
        private bool Validate(string Path)
        {
            if (string.IsNullOrWhiteSpace(Path))
                Path = Environment.CurrentDirectory;
            //throw new ArgumentNullException(Path, "Path is NULL.");

            if (!IsPathExists(Path))
            {
                CreateDirectory(Path);
                return true;
            }

            return true;
        }

        private static IEnumerable<string> GetFilesList(string path, string formats)
        {
            var formatsLower = formats.Split(' ', ',', '\t');
            return Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                .Where(s => formatsLower.Contains(Path.GetExtension(s).ToLowerInvariant()
                    .Trim()));
        }

        public void SetMessage(string message)
        {
            if (message.Length > 0)
            {
                GetMessage = message;
                OnPropertyChanged("MessageChange");
            }
            else
            {
                GetMessage = "...";
                OnPropertyChanged("MessageChange");
            }
        }

        #endregion
    }
}