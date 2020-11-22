using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Storage
{
    /// <summary>
    /// РК2 Задание № 1 - Agile
    /// </summary>
    public class Exercise3 : Exercise0n
    {
        /// <summary>
        /// Принцип Agile (один из 12)
        /// </summary>
        [Required()]
        public virtual Principle Principle { get; set; }

        /// <summary>
        /// Порядковый номер принципа в задании
        /// </summary>
        [DisplayName("№")]
        public int Number { get; set; }       
    }
}