using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

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
        /// Конструктор 
        /// </summary>
        public BotService()
        {
            InitializeComponent();
            bot = new Bot();
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
            Start();
        }

        /// <summary>
        /// Событие останова сервиса
        /// </summary>
        protected override void OnStop()
        {
            bot.Stop();
        }

        /// <summary>
        /// Событие приостанова сервиса
        /// </summary>
        protected override void OnPause()
        {
            bot.Stop();
        }

        /// <summary>
        /// Событие возобновления сервиса
        /// </summary>
        protected override void OnContinue()
        {
            bot.Start();
        }

    }
}
