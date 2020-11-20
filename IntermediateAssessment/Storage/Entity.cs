using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace IntermediateAssessment.Storage
{
    /// <summary>
    /// Сущность базы данных
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Уникальный идентификатор записи
        /// </summary>       
        [Key()]
        public Guid ID { get; set; }

        /// <summary>
        /// Конструктор по умолчаению
        /// </summary>
        public Entity()
        {
            // Генерация нового уникального идентификатора
            ID = Guid.NewGuid();
        }
    }
}
