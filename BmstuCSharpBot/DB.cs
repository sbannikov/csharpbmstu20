using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmstuCSharpBot
{
    /// <summary>
    /// База данных - прикладная часть
    /// </summary>
    partial class DB : IDatabase
    {
        public User GetUser(string phone)
        {
            var user = Students.Where(a => a.Phone == phone).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            else
            {
                var u = new User()
                {
                    FileName = user.FileName,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    Group = user.Group,
                    LastName = user.LastName,
                    Phone = user.Phone
                };
                return u;
            }
        }

        /// <summary>
        /// Протоколирование сообщения в базу
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void WriteMessage(string message)
        {
            var m = new Messages()
            {
                Message = message
            };
            Messages.Add(m);
            SaveChanges();
        }
    }
}
