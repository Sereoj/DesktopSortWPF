namespace Test.Models
{
    internal partial class FileManager
    {
        public enum FileMode : byte
        {
            Copy, // Копирование
            Move, // Перемещение
            Ignore // Игнорирование, использование в качестве тестирования функционала, игнорирования работы с файлами. 
        }
    }
}