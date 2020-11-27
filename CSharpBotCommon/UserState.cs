using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmstuCSharpBot
{
    /// <summary>
    /// Состояние пользователя
    /// </summary>
    public enum UserState
    {
        /// <summary>
        /// Основное состояние
        /// </summary>
        Base,
        /// <summary>
        /// Ожидание телефона пользователя
        /// </summary>
        Contact
    }
}
