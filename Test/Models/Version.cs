using System.Windows.Forms;

namespace Test.Models
{
    /// <summary>
    ///     Получение информации о продукте.
    /// </summary>
    internal class Version
    {
        public enum StageVersion
        {
            PreAlpha,
            Alpha,
            Beta,
            Edition
        }

        private static Version _model;

        public StageVersion State = StageVersion.Beta;

        public static Version Model => _model ??= new Version();

        /// <summary>
        ///     Версия продукта.
        /// </summary>
        /// <returns></returns>
        public string GetVersion(bool isGetState)
        {
            return isGetState == false
                ? Application.ProductVersion.Substring(0, 3)
                : Application.ProductVersion.Substring(0, 3) + State;
        }

        public string Name(string name, bool isVersion, bool isGetState)
        {
            return isVersion ? name : name + " " + GetVersion(isGetState);
        }
    }
}