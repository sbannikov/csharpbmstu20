using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmstuCSharpBot
{
    partial class Messages
    {
        public Messages()
        {
            // Генерация уникального идентификатора
            ID = Guid.NewGuid();
        }
    }
}
