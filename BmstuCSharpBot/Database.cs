using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BmstuCSharpBot
{
    /// <summary>
    /// База данных
    /// </summary>
    public class Database : IDatabase
    {
        /// <summary>
        /// Соединение с базой данных
        /// </summary>
        private SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Database=BOT;Integrated Security=SSPI;");
            conn.Open();
        }

        /// <summary>
        /// Проверка на существование телефона в БД
        /// </summary>
        /// <param name="phone">Номер телефона</param>
        /// <returns></returns>
        public bool IsPhone(string phone)
        {
            SqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT [Телефон] FROM [Students] WHERE [Телефон] = @phone";
            cmd.Parameters.AddWithValue("phone", phone);
            object result = cmd.ExecuteScalar();
            return result != null;
        }

        /// <summary>
        /// Найти пользователя по номеру телефона
        /// </summary>
        /// <param name="phone">Номер телефона</param>
        /// <returns>Пользователь или null, если пользователь не найден</returns>
        public User GetUser(string phone)
        {
            SqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT [Фамилия], [Имя], [Группа], [Личное дело], [E-mail] FROM [Students] WHERE [Телефон] = @phone";
            cmd.Parameters.AddWithValue("phone", phone);
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    var user = new User()
                    {
                        LastName = rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0),
                        FirstName = rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1),
                        Group = rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2),
                        FileName = rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3),
                        Email = rdr.IsDBNull(4) ? string.Empty : rdr.GetString(4)
                    };
                    return user;
                }
            }
            return null;
        }

        /// <summary>
        /// Запись сообщения в базу данных
        /// </summary>
        /// <param name="message"></param>
        public void WriteMessage(string message)
        {
            SqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Messages (Message) VALUES (@message)";
            cmd.Parameters.AddWithValue("message", message);
            if (cmd.ExecuteNonQuery() != 1)
            {
                throw new Exception("Ошибка при записи сообщения в базу");
            }
        }
    }
}
