using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Storage
{
    /// <summary>
    /// Задание № 2 РК 1
    /// </summary>
    public class Exercise2 : Entity
    {
        /// <summary>
        /// Задание студенту на РК
        /// </summary>
        public virtual Exercise Exercise { get; set; }

        /// <summary>
        /// Строка кода
        /// </summary>
        public virtual CodeRow CodeRow { get; set; }

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