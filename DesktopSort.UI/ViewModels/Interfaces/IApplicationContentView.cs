using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ViewModels.Interfaces
{
    interface IApplicationContentView
    {

        /// <summary>
        /// Заголовок
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Загрузка
        /// </summary>
        bool IsLoading { get; set; }
        /// <summary>
        /// Выполнение
        /// </summary>
        void Init();
    }
}
