using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Storage
{
    /// <summary>
    /// РК 1 задание № 2
    /// </summary>
    public class Exercise2 : Exercise0
    {
        /// <summary>
        /// Строка кода
        /// </summary>
        public virtual CodeRow CodeRow { get; set; }

        /// <summary>
        /// Исправленная строка кода
        /// </summary>
        [MaxLength(255)]
        public string AnswerString { get; set; }

        /// <summary>
        /// Представление строки кода для HTML
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(CodeRow.Code) ? "&nbsp;" : CodeRow.Code.Replace(" ", "&nbsp;");
        }
    }
}