using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Storage
{
    public class Assessment : Entity
    {
        /// <summary>
        /// Номер рубежного контроля
        /// </summary>
        [DisplayName("Номер РК")]
        public int Number { get; set; }

        /// <summary>
        /// Наименование рубежного контроля
        /// </summary>
        [DisplayName("Название РК")]
        [MaxLength(255)]
        [Required()]
        public string Name { get; set; }

        /// <summary>
        /// Время начала рубежного контроля
        /// </summary>
        public DateTime StartTime { get; set; }
    }
}