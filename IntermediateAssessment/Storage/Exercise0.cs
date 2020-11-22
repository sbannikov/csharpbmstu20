using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Storage
{
    /// <summary>
    /// Индивидуальное задание студенту на РК
    /// </summary>
    public class Exercise0 : Entity
    {
        /// <summary>
        /// Задание студенту на РК
        /// </summary>
        [Required()]
        public virtual Exercise Exercise { get; set; }

        /// <summary>
        /// Признак корректного ответа
        /// </summary>
        public bool? Correct { get; set; }
    }
}