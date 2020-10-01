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
        /// Группа для отображения в HTML
        /// </summary>
        [DisplayName("Номер группы")]
        public string GroupHtml
        {
            // Используем неразрывный дефис
            get { return Group.Replace("-", "&#8209;"); }
        }

        /// <summary>
        /// Личное дело
        /// </summary>
        [DisplayName("Номер личного дела")]
        [MaxLength(16)]
        [Required()]
        public string FileNumber { get; set; }
    }
}