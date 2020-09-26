using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntermediateAssessment.Storage
{
    /// <summary>
    /// Задание № 1 РК1
    /// </summary>
    public class Exercise1 : Entity
    {
        /// <summary>
        /// Задание студенту на РК
        /// </summary>
        public virtual Exercise Exercise { get; set; }

        /// <summary>
        /// Имя сотрудика
        /// </summary>
        public virtual Character Character { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// Первая способность
        /// </summary>
        public virtual Ability Ability1 { get; set; }

        /// <summary>
        /// Вторая способность
        /// </summary>
        public virtual Ability Ability2 { get; set; }

        /// <summary>
        /// Уникальный код задания
        /// </summary>
        [MaxLength(10)]
        [Required()]
        public string Code { get; set; }

        /// <summary>
        /// Формирование кода задания
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Role.Number,2:00}{Character.Number,2:00}{Ability1.Number,2:00}{Ability2.Number,2:00}";
        }
    }
}
