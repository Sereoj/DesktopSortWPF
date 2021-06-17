﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Test.Models.Settings;
using Test.ViewModels.Base;
using Test.Models.FileManagerModel;

namespace Test.ViewModels
{
    public partial class FileManagerVM : ViewModel
    {

        #region закрытые поля

        private string INPUT_PATH;
        private string OUTPUT_PATH;

        private readonly int delay = 100;

        public MessengerVM MessengerVM { get; }
        public IconChangerVM IconChanger { get; }

        #endregion

        public enum FileMode : byte
        {
            Copy, // Копирование
            Move, // Перемещение
            Ignore // Игнорирование, использование в качестве тестирования функционала, игнорирования работы с файлами. 
        }

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

        public void DeleteDirectory(string path,bool directory = false)
        {
            if ( directory )
                path.DeleteDirectory();
        }

        /*
        * Поиск файлов небезопасный, получение данных исключительно от программы.
        * Первая функция.
        */
        public async Task SearchFiles(List<BasicConfig> Files, FileMode modeFile)
        {
            foreach (BasicConfig file in Files)
            {
                await Task.Delay(delay);
                await SearchFilesAsyn(file, modeFile);
            }

            SetMessage("Работа завершилась!");
        }

        /*
        * Поиск файлов небезопасный, получение данных исключительно от программы.
        * Вторая функция.
        */
        public async Task SearchFilesAsyn(BasicConfig config, FileMode modeFile)
        {
            string PathNewDirectory = @config.Catalog;
            string PatternExtension = config.Extension;
            string Icon = @config.IconPath;

            var NewDirectory = Path.Combine(OUTPUT_PATH, PathNewDirectory);
            SetMessage("Начало выполнения: " + PathNewDirectory);
            await Task.Delay(delay);

            try
            {
                var files = GetFilesList(INPUT_PATH, PatternExtension);

                if (files != null && NewDirectory.IsPathExists() && files.ToList().Count > 0 )
                {
                    NewDirectory.CreateDirectory();
                    IconChanger.FolderIcon(NewDirectory, Icon);
                }

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
            catch{}

            SetMessage("Задача завершена!");
            await Task.Delay(delay);
        }
        #endregion

        #region Закрытые методы

        private void SetMessage(string message)
        {
            MessengerVM.SetMessage(message);
        }

        /// <summary>Валидация пути</summary>
        /// <param name="Path"></param>
        private bool Validate(string Path)
        {
            if (string.IsNullOrWhiteSpace(Path))
                Path = Environment.CurrentDirectory;
            //throw new ArgumentNullException(Path, "Path is NULL.");

            if (Path.IsPathExists())
            {
                Path.CreateDirectory();
                return true;
            }
            return true;
        }

        private static IEnumerable<string> GetFilesList(string path, string formats)
        {
            var formatsLower = formats.Split(' ', ',', '\t');
            return Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                .Where(s => formatsLower.Contains(Path.GetExtension(s)
                    ?.ToLowerInvariant()
                    .Trim()));
        }

        #endregion


        public FileManagerVM(MessengerVM messengerVM, IconChangerVM iconChanger)
        {
            MessengerVM = messengerVM;
            IconChanger = iconChanger;
        }
    }
}