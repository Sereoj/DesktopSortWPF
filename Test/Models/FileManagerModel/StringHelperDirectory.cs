﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models.FileManagerModel
{
    internal static class StringHelperDirectory
    {
        public static void CreateDirectory(this string Path)
        {
            Directory.CreateDirectory(Path);
        }

        /// <summary>Проверка директории</summary>
        /// <param name="Path"></param>
        public static bool IsPathExists(this string Path) => !Directory.Exists(Path);

        /// <summary>Удаление директории</summary>
        /// <param name="Path"></param>
        public static void DeleteDirectory(this string Path)
        {
            Directory.Delete(Path);
        }
    }
}
