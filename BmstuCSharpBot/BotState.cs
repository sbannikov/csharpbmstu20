using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using NLog;

namespace BmstuCSharpBot
{
    /// <summary>
    /// Состояние бота
    /// </summary>
    [XmlRoot(ElementName = "BotState", Namespace = "http://www.bmstu.ru/csharp")]
    public class BotState
    {
        /// <summary>
        /// Журналирование
        /// </summary>
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Массив пользователей
        /// </summary>
        [XmlElement(ElementName = "User")]
        public User[] Users;

        public static BotState Load(string name)
        {
            try
            {
                // Объект для сериализации
                XmlSerializer ser = new XmlSerializer(typeof(BotState));

                // Открыть файл для чтения XML-данных
                using (XmlReader rdr = XmlReader.Create(name))
                {
                    // Десериализация и формирование объекта в памяти
                    return (BotState)ser.Deserialize(rdr);
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                // На всякий случай сообщим
                log.Warn($"Файл {name} не найден, будет создан новый файл.");
                // Создадим новое пустое состояние
                return new BotState();
            }
            catch (Exception ex)
            {
                // На всякий случай сообщим
                log.Warn(ex);
                // Создадим новое пустое состояние
                return new BotState();
            }

        }

        /// <summary>
        /// Сериализация объекта в файл
        /// </summary>
        /// <param name="name">Имя файла</param>
        public void Save(string name)
        {
            // Объект для сериализации
            XmlSerializer ser = new XmlSerializer(typeof(BotState));
            // Настройка человекочитаемого формирования XML-файла
            XmlWriterSettings s = new XmlWriterSettings()
            {
                Indent = true // каждый тег на своей строке
            };
            // Писатель в файл
            using (XmlWriter wrt = XmlWriter.Create(name, s))
            {
                // Собственно сериализация
                ser.Serialize(wrt, this);
            }
        }

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        public void AddUser(User user)
        {
            if (Users == null)
            {
                Users = new User[] { user };
            }
            else
            {
                var list = Users.ToList();
                list.Add(user);
                Users = list.ToArray();
            }
        }
    }
}
