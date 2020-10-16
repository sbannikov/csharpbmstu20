using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
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
            // Первый параметр командной строки в нижнем регистре
            string arg1 = (args.Length > 0) ? args[0] : "";
            arg1 = arg1.ToLower();

            // Сам сервис
            BotService svc;

            // Имя исполняемого файла сервиса
            // Примечание: GetExecutingAssembly - текущая сборка, но не главный исполняемый файл
            string name = Assembly.GetEntryAssembly().Location;

            switch (arg1)
            {
                case "install":
                    // Установка сервиса операционной системы
                    ManagedInstallerClass.InstallHelper(new string[] { name });
                    break;

                case "delete":
                case "remove":
                case "uninstall":
                    // Установка сервиса операционной системы
                    ManagedInstallerClass.InstallHelper(new string[] { "/u", name });
                    break;

                case "console":
                    svc = new BotService();
                    svc.Start();
                    Console.WriteLine("Бот запущен. Для останова нажмите Enter");
                    Console.ReadLine();
                    break;

                case "":
                    svc = new BotService();
                    ServiceBase.Run(svc);
                    break;

                default:
                    Console.WriteLine($"Параметр {arg1} не поддерживается");
                    break;
            }


            return 0;
        }
    }
}