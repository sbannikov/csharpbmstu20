﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Storage
{
    /// <summary>
    /// РК 1 задание № 2
    /// </summary>
    public class Exercise2 : Entity
    {
        /// <summary>
        /// Задание студенту на РК
        /// </summary>
        [Required()]
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