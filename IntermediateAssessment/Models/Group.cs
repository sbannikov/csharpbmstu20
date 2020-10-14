using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Models
{
    public class Group
    {
        /// <summary>
        /// Группа
        /// </summary>
        [DisplayName("Номер группы")]
        [MaxLength(16)]
        public string GroupName { get; set; }

        /// <summary>
        /// Список студентов в группе
        /// </summary>
        public List<Student> Students = new List<Student>();
    }
}