using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BmstuCSharpBot
{
    /// <summary>
    /// Управление ботом
    /// </summary>
    [ServiceBehavior
        (InstanceContextMode = InstanceContextMode.Single,
         ConcurrencyMode = ConcurrencyMode.Single)]
    public class ControlService : IControlService
    {
        /// <summary>
        /// Управляемый бот
        /// </summary>
        private Bot bot;

        public ControlService(Bot bot)
        {
            this.bot = bot;
        }

        /// <summary>
        /// Запрос текущего состояния
        /// </summary>
        /// <returns></returns>
        public string Query()
        {
            return "У меня все хорошо";
        }

        public void StartTrace(string ip)
        {
            bot.StartTrace(ip);            
        }

        public void StopTrace()
        {
            bot.StopTrace();
        }
    }
}
