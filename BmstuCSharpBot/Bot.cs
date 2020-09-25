﻿using System;
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
        /// Имя локального файла для хранения состояния бота
        /// </summary>
        private const string stateFileName = @"c:\state.xml";

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
                // Определение пользователя
                var u = state.Users?.FirstOrDefault(a => a.ID == m.Chat.Id);

                if (u != null)
                {
                    // Проверка текущего состояния пользователя
                    switch (u.State)
                    {
                        case UserState.Contact:
                            client.SendTextMessageAsync(m.Chat.Id, $"Я жду номер телефона");
                            return;
                    }
                }

                client.SendTextMessageAsync(m.Chat.Id, $"Ты сказал мне: {m.Text}");
            }
        }

        /// <summary>
        /// Обработка присылаемых контактов
        /// </summary>
        /// <param name="m"></param>
        public void ContactProcessor(Telegram.Bot.Types.Message m)
        {
            // Поиск пользователя по идентификатору
            var u = state.Users?.FirstOrDefault(a => a.ID == m.Chat.Id);

            // Проверка на существование пользователя
            if (u == null)
            {
                // Ответ незнакомцу
                client.SendTextMessageAsync(m.Chat.Id, $"Я не разговариваю с незнакомцами, используй команду /start для знакомства");
            }
            else
            {
                if (u.State != UserState.Contact)
                {
                    client.SendTextMessageAsync(m.Chat.Id, $"Мне сейчас не нужно это");
                    return;
                }
                // Проверка на подмену контактной информации
                if (m.Chat.Id != m.Contact.UserId)
                {
                    client.SendTextMessageAsync(m.Chat.Id, $"Не пытайтесь меня взломать!", replyMarkup: null);
                    return;
                }
                // Сохранение телефона пользователя
                u.Phone = m.Contact.PhoneNumber;
                u.State = UserState.Base;
                state.Save(stateFileName);

                // Ответ пользователю
                client.SendTextMessageAsync(m.Chat.Id, $"Спасибо за номер телефона!");
            }
        }

        /// <summary>
        /// Команда /start
        /// </summary>
        /// <param name="m"></param>
        public void startCommand(Telegram.Bot.Types.Message m)
        {
            // Поиск пользователя по идентификатору
            var u = state.Users?.FirstOrDefault(a => a.ID == m.Chat.Id);

            // Проверяем, что пользователь полностью зарегистрирован
            if ((u != null) && !string.IsNullOrEmpty(u.Phone))
            {
                // Пользователь уже есть в базе
                // Ответ пользователю
                client.SendTextMessageAsync(m.Chat.Id, $"Привет, {m.Chat.FirstName}, с возвращением!");
            }
            else // Обнаружен новый пользователь
            {
                // Ответ пользователю
                var button = new Telegram.Bot.Types.ReplyMarkups.KeyboardButton("Телефон")
                {
                    RequestContact = true
                };
                var kbd = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup(button);
                kbd.OneTimeKeyboard = true;
                kbd.ResizeKeyboard = true;

                client.SendTextMessageAsync(m.Chat.Id, $"Привет, {m.Chat.FirstName}, рад знакомству!", replyMarkup: kbd);

                // Создание нового пользователя
                if (u == null)
                {
                    u = new User()
                    {
                        ID = m.Chat.Id,
                        UserName = m.Chat.Username,
                        FirstName = m.Chat.FirstName,
                        LastName = m.Chat.LastName,
                    };
                    state.AddUser(u);
                }
                // Переход в состояние ожидания номера телефона
                u.State = UserState.Contact;
                // Сохранить состояние бота
                state.Save(stateFileName);
            }
        }

        /// <summary>
        /// Запуск бота
        /// </summary>
        internal void Run()
        {
            state = BotState.Load(stateFileName);
            client.StartReceiving();
        }
    }
}
