using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test.ViewModels.Base;

namespace Test.Models
{ 
    internal class FileManager: ViewModel
    {

        #region закрытые поля
        private string INPUT_PATH;
        private string OUTPUT_PATH;
        private bool DeleteDefaultDirectory;
        private string Message;
        private int delay = 100;
        #endregion


        public string GetMessage { get => Message;}

        static FileManager _model;
        public static FileManager Model => _model ?? (_model = new FileManager());

        public enum FileMode : byte
        {
            Copy, // Копирование
            Move, // Перемещение
            Ignore // Игнорирование, использование в качестве тестирования функционала, игнорирования работы с файлами. 
        }

        public FileManager()
        {
        }


        #region Публичные методы

        public void SetInput(string input)
        {
            if (Validate(input))
            {
                INPUT_PATH = input;
                SetMessage(input);
            }
        }

        public void SetOutput(string output)
        {
            if (Validate(output))
            {
                OUTPUT_PATH = output;
                SetMessage(output);
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
        public async Task SearchFiles(List<Setting> Files, FileMode modeFile)
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
            if (IsPathExists(NewDirectory))
                CreateDirectory(NewDirectory);

            SetMessage("Начало выполнения: " + PathNewDirectory);
            await Task.Delay(delay);

            if (DeleteDefaultDirectory != false)
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
        private bool IsPathExists(string Path)
        {
            return !Directory.Exists(Path) ? true : false;
        }
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

        private void SetMessage(string message)
        {
            if( message.Length > 0 )
            {
                Message = message;
                OnPropertyChanged("MessageChange");
            }
            else
            {
                Message = "...";
                OnPropertyChanged("MessageChange");
            }
        }
        #endregion
    }
}
