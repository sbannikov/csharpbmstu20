using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace BmstuCSharpBot
{
    /// <summary>
    /// Класс для точки запуска приложения
    /// </summary>
    class Program
    {
        /// <summary>
        /// Журналирование
        /// </summary>
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Точка запуска приложения
        /// </summary>
        /// <param name="args">Список параметров командной строки</param>
        /// <returns>Всегда возвращает 0</returns>
        static int Main(string[] args)
        {
            try
            {
                log.Trace("-> Main");

                // Первый параметр командной строки в нижнем регистре
                string arg1 = (args.Length > 0) ? args[0] : "";
                arg1 = arg1.ToLower();

                // Сам сервис
                BotService svc;

                // Имя исполняемого файла сервиса
                // Примечание: GetExecutingAssembly - текущая сборка, но не главный исполняемый файл
                string name = Assembly.GetEntryAssembly().Location;

                // Номер версии файла                        
                string version = FileVersionInfo.GetVersionInfo(name).FileVersion.ToString();

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
                        log.Info($"Бот запущен в консольном режиме, версия {version}"); ;
                        break;

                    case "":
                        svc = new BotService();
                        ServiceBase.Run(svc);
                        break;

                    default:
                        log.Fatal($"Параметр {arg1} не поддерживается");
                        if (Environment.UserInteractive)
                        {
                            // [!] написать вывод подсказки по параметрам
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
            }
            finally
            {
                if (Environment.UserInteractive)
                {
                    Console.WriteLine("Нажмите Enter для завершения программы");
                    Console.ReadLine();
                }
            }

            return 0;
        }
    }
}