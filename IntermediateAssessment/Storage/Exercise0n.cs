using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Storage
{
    /// <summary>
    /// Индивидуальное задание студенту на РК с числовым ответом
    /// </summary>
    public class Exercise0n : Exercise0
    {
        /// <summary>
        /// Ответ на задание - номер сотрудника
        /// </summary>
        public int? AnswerNumber { get; set; }

        /// <summary>
        /// Сообщение об ошибке при вводе номера сотрудника
        /// </summary>
        [NotMapped()]
        public string AnswerNumberMessage { get; set; }
    }
}