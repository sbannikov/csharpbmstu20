﻿using System;
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
        /// Признак сданного РК1
        /// [!] переписать для многих РК
        /// </summary>
        public bool Passed
        {
            get { return Exercises.FirstOrDefault(x => x.FinishTime != null) != null; }
        }

        /// <summary>
        /// Отображаемый статус РК1
        /// [!] переписать для многих РК
        /// </summary>
        [DisplayName("Статус РК1")]
        public string Status
        {
            get { return Passed ? "сдан" : dash; }
        }
    }
}