using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Storage
{
    /// <summary>
    /// Студент
    /// </summary>
    public class Student : Entity
    {
        /// <summary>
        /// Неразрывный дефис
        /// </summary>
        public const string Dash = "&#8209;";

        /// <summary>
        /// Имя
        /// </summary>
        [DisplayName("Имя")]
        [MaxLength(255)]
        [Required()]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [DisplayName("Фамилия")]
        [MaxLength(255)]
        [Required()]
        public string LastName { get; set; }

        /// <summary>
        /// Группа
        /// </summary>
        [DisplayName("Номер группы")]
        [MaxLength(16)]
        [Required()]
        public string Group { get; set; }

        /// <summary>
        /// Личное дело
        /// </summary>
        [DisplayName("Номер личного дела")]
        [MaxLength(16)]
        [Required()]
        public string FileNumber { get; set; }

        /// <summary>
        /// Порядковый номер в списке группы (для ЭУ)
        /// </summary>
        [DisplayName("№ п/п")]
        public int ListNumber { get; set; }

        /// <summary>
        /// Список РК
        /// </summary>
        public virtual HashSet<Exercise> Exercises { get; set; }

        /// <summary>
        /// Группа для отображения в HTML
        /// </summary>
        [DisplayName("Номер группы")]
        public string GroupHtml
        {
            // Используем неразрывный дефис
            get { return Group.Replace("-", Dash); }
        }

        /// <summary>
        /// Задание с заданным номером.
        /// Ищется сначала выполненное задание, потом - любое (только начатое)
        /// </summary>
        /// <param name="n">Номер задания</param>
        /// <returns>Задание или null, если задание отсутствует</returns>
        public Exercise Exercise(int n)
        {
            Exercise e = Exercises.FirstOrDefault(x => x.FinishTime != null && x.Assessment.Number == n);
            if (e != null)
            {
                return e;
            }
            e = Exercises.FirstOrDefault(x => x.Assessment.Number == n);
            return e;
        }
    }
}