using System;
using System.Collections.Generic;
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
        [MaxLength(255)]
        [Required()]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [MaxLength(255)]
        [Required()]
        public string LastName { get; set; }

        /// <summary>
        /// Группа
        /// </summary>
        [MaxLength(16)]
        [Required()]
        public string Group { get; set; }

        /// <summary>
        /// Личное дело
        /// </summary>
        [MaxLength(16)]
        [Required()]
        public string FileNumber { get; set; }
    }
}