using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace BmstuCSharpBot
{
    /// <summary>
    /// Сервис
    /// </summary>
    partial class BotService : ServiceBase
    {
        /// <summary>
        /// Бот
        /// </summary>
        private Bot bot;

        /// <summary>
        /// Журналирование
        /// </summary>
        private readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Конструктор 
        /// </summary>
        public BotService()
        {
            InitializeComponent();
            bot = new Bot();
        }

        /// <summary>
        /// Протоколирование сообщения
        /// </summary>
        /// <param name="s">Сообщение</param>
        /// <param name="entryType">Тип сообщения</param>
        /// <param name="id">Идентификатор сообщения</param>
        private void WriteMessage(EventID id, string s, LogLevel entryType)
        {
            // Протоколирование с сохранением идентификатора события для журнала Windows
            var logEvent = new LogEventInfo(entryType, null, s);
            logEvent.Properties.Add("EventID", (int)id);
            log.Log(logEvent);
        }

        /// <summary>
        /// Запуск сервиса
        /// </summary>
        internal void Start()
        {
            bot.Start();
        }

        /// <summary>
        /// Событие запуска сервиса
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            try
            {
                Start();
                WriteMessage(EventID.StartService, "Сервис успешно запущен", LogLevel.Info);
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
            }
        }

        /// <summary>
        /// Событие останова сервиса
        /// </summary>
        protected override void OnStop()
        {
            try
            {
                bot.Stop();
                WriteMessage(EventID.StopService, "Сервис успешно остановлен", LogLevel.Info);
            }
            catch (Exception ex)
            {
                log.Warn(ex);
            }
        }

        /// <summary>
        /// Событие приостанова сервиса
        /// </summary>
        protected override void OnPause()
        {
            try
            {
                bot.Stop();
                WriteMessage(EventID.PauseService, "Сервис успешно приостановлен", LogLevel.Info);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// Событие возобновления сервиса
        /// </summary>
        protected override void OnContinue()
        {
            try
            {
                bot.Start();
                WriteMessage(EventID.ContinueService, "Сервис успешно возобновлён", LogLevel.Info);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}
