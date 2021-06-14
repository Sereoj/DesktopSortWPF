﻿using System.Windows.Forms;

namespace Test.Models
{
    /// <summary>
    ///     Получение информации о продукте.
    /// </summary>
    public class Version
    {
        public Version() { }
        public enum StageVersion
        {
            PreAlpha,
            Alpha,
            Beta,
            Edition,
            Stable,
            Final
        }

        public static StageVersion State = StageVersion.Alpha;

        /// <summary>
        ///     Версия продукта.
        /// </summary>
        /// <returns></returns>
        public string Get(bool isGetState)
        {
            return isGetState == false
                ? Application.ProductVersion.Substring(0, 3)
                : Application.ProductVersion.Substring(0, 3) + State;
        }
    }
}