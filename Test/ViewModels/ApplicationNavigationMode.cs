using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ViewModels
{
    enum ApplicationNavigationMode
    {
        /// <summary>
        /// Main - Основной режим (не скрытый от пользователя)
        /// Dev - Режим тестирования, скрытый от пользователя.
        /// </summary>
        Main,
        Dev
    }
}
