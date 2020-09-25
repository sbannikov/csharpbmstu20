using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BmstuCSharpBot
{
    /// <summary>
    /// Состояние бота
    /// </summary>
    public class BotState
    {
        /// <summary>
        /// Массив пользователей
        /// </summary>
        public User[] Users;

        public static BotState Load(string name)
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
