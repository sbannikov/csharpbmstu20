using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Storage
{
    /// <summary>
    /// Принцип Agile (РК2)
    /// </summary>
    public class Principle : Entity
    {
        /// <summary>
        /// Номер принципа, от 1 до 12
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Формулировка принципа
        /// </summary>
        [DisplayName("Принцип Agile")]
        [MaxLength(255)]
        [Required()]
        public string Name { get; set; }
    }
}