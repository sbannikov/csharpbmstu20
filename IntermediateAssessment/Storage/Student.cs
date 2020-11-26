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
        private const string dash = "&#8209;";

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
            get { return Group.Replace("-", dash); }
        }

        /// <summary>
        /// Признак сданного РК
        /// null - не было попытки сдачи
        /// false - РК начат, но не отправлен
        /// true - РК отправлен
        /// </summary>
        public bool? Passed(int n)
        {
            {
                if (Exercises.Any(x => x.FinishTime != null && x.Assessment.Number == n))
                {
                    return true;
                }
                else if (Exercises.Any(x => x.Assessment.Number == n))
                {
                    return false;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Отображаемый статус РК
        /// </summary>
        [DisplayName("Статус РК")]
        public string Status(int n)
        {
            {
                bool? passed = Passed(n);
                return passed.HasValue ? (passed.Value ? "сдан" : "в процессе") : dash;
            }
        }
    }
}