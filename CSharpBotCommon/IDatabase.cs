using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmstuCSharpBot
{
    /// <summary>
    /// Хранение данных в базе
    /// </summary>
    public interface IDatabase
    {
        User GetUser(string phone);
        void WriteMessage(string message);
    }
}
