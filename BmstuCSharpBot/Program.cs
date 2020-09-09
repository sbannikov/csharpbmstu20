using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmstuCSharpBot
{
    /// <summary>
    /// Класс для точки запуска приложения
    /// </summary>
    class Program
    {
        /// <summary>
        /// Точка запуска приложения
        /// </summary>
        /// <param name="args">Список параметров командной строки</param>
        /// <returns>Всегда возвращает 0</returns>
        static int Main(string[] args)
        {
            var bot = new Bot();
            bot.Run();
            Console.WriteLine("Бот запущен. Для останова нажмите Enter");
            Console.ReadLine();
            return 0;
        }
    }
}