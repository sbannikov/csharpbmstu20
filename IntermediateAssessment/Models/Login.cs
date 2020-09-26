using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Models
{
    /// <summary>
    /// Данные для входа
    /// </summary>
    public class Login
    {
        /// <summary>
        /// Номер личного дела
        /// </summary>
        [DisplayName("Номер личного дела")]
        public string FileNumber { get; set; }
    }
}