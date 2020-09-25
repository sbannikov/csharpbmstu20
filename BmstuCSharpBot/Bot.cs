using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Telegram.Bot;

namespace BmstuCSharpBot
{
    /// <summary>
    /// Бот для Telegram
    /// </summary>
    internal class Bot
    {
        /// <summary>
        /// Клиент для Telegram
        /// </summary>
        private readonly TelegramBotClient client;

        /// <summary>
        /// Журналирование
        /// </summary>
        private readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Полное состояние бота
        /// </summary>
        private BotState state = new BotState();

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        internal Bot()
        {
            client = new TelegramBotClient("410878642:AAEQJ6qcQxRvNQu3MQUAOpfrjo4nYn1UuEA");
            client.OnMessage += MessageProcessor;
        }

        /// <summary>
        /// Обработчик события - принятое сообщение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageProcessor(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            try
            {
                log.Trace("-> MessageProcessor");
                log.Debug($"{e.Message.Type} {e.Message.Text}");

                // Имя метода, который надо вызвать
                string name = $"{e.Message.Type}Processor";

                // Поиск метода класса
                MethodInfo method = GetType().GetMethod(name);

                // Проверка на наличие метода
                if (method == null)
                {
                    // Метод не найден (имеет смысл записать в протокол)
                    client.SendTextMessageAsync(e.Message.Chat.Id, $"Ты мне отправил {e.Message.Type}, но я пока этого не понимаю");
                }
                else
                {
                    // Вызов метода с единственным параметром
                    method.Invoke(this, new object[] { e.Message });
                }
            }
            catch (Exception ex)
            {
                log.Warn(ex);
            }
            finally
            {
                log.Trace("<- MessageProcessor");
            }
        }

        /// <summary>
        /// Обработка текстовых сообщений
        /// </summary>
        /// <param name="m"></param>
        public void TextProcessor(Telegram.Bot.Types.Message m)
        {
            // Проверка на команду
            if (m.Text.Substring(0, 1) == "/")
            {
                // Это команда - переходим к вызову метода команды
                // Имя метода, который надо вызвать
                string cmd = m.Text.Substring(1).ToLower();
                string name = $"{cmd}Command";

                // Поиск метода класса
                MethodInfo method = GetType().GetMethod(name);

                // Проверка на наличие метода
                if (method == null)
                {
                    // команда не найдена (имеет смысл записать в протокол)
                    client.SendTextMessageAsync(m.Chat.Id, $"Я пока не понимаю команду {cmd}");
                }
                else
                {
                    // Вызов метода с единственным параметром
                    method.Invoke(this, new object[] { m });
                }
            }
            else
            {
                client.SendTextMessageAsync(m.Chat.Id, $"Ты сказал мне: {m.Text}");
            }
        }

        /// <summary>
        /// Команда /start
        /// </summary>
        /// <param name="m"></param>
        public void startCommand(Telegram.Bot.Types.Message m)
        {
            // Ответ пользователя
            client.SendTextMessageAsync(m.Chat.Id, $"Привет, {m.Chat.FirstName}, рад знакомству!");

            User user = new User()
            {
                ID = m.Chat.Id
            };
            state.AddUser(user);
            state.Save(@"c:\state.xml");
        }

        /// <summary>
        /// Запуск бота
        /// </summary>
        internal void Run()
        {
            state = BotState.Load(@"c:\state.xml");
            client.StartReceiving();
        }
    }
}
