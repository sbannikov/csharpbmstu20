using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Models
{
    /// <summary>
    /// Строка для отчета
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Имя
        /// </summary>
        [DisplayName("Имя")]
        [MaxLength(255)]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [DisplayName("Фамилия")]
        [MaxLength(255)]
        public string LastName { get; set; }

      
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
        /// Отображаемый статус РК
        /// </summary>
        [DisplayName("Статус ")]
        public List<MatrixItem> Status { get; set; }

        public Student(Storage.Student student, int count)
        {
            FileNumber = student.FileNumber;
            FirstName = student.FirstName;
            LastName = student.LastName;
            ListNumber = student.ListNumber;
            Status = new List<MatrixItem>();
            for (int n = 1; n <= count; n++)
            {
                Status.Add(new MatrixItem(student.Exercise(n)));
            }
        }
    }
}