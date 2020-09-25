using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BmstuCSharpBot
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя в Telegram
        /// </summary>
        [XmlAttribute()]
        public long ID;

        /// <summary>
        /// Учетное имя пользователя
        /// </summary>
        [XmlElement(ElementName = "Name")]
        public string UserName;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [XmlElement(ElementName = "FirstName")]
        public string FirstName;

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [XmlElement(ElementName = "LastName")]
        public string LastName;

        /// <summary>
        /// Телефон пользователя
        /// </summary>
        [XmlElement(ElementName = "Phone")]
        public string Phone;

        /// <summary>
        /// Состояние пользователя
        /// </summary>
        [XmlAttribute()]
        public UserState State;
    }
}
