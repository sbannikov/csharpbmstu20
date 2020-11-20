using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Storage
{
    /// <summary>
    /// РК2 Задание № 1 - Agile
    /// </summary>
    public class Exercise3 : Entity
    {
        /// <summary>
        /// Задание студенту на РК
        /// </summary>
        [Required()]
        public virtual Exercise Exercise { get; set; }

        /// <summary>
        /// Принцип Agile (один из 12)
        /// </summary>
        [Required()]
        public virtual Principle Principle { get; set; }

        /// <summary>
        /// Порядковый номер принципа в задании
        /// </summary>
        public int Number { get; set; }
    }
}