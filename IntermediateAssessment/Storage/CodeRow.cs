using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntermediateAssessment.Storage
{
    /// <summary>
    /// Строка кода
    /// </summary>
    public class CodeRow : Entity
    {
        /// <summary>
        /// Рубежный контроль
        /// </summary>
        [Required()]
        public virtual Assessment Assessment { get; set; }

        /// <summary>
        /// Номер строки кода
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Вариант строки кода
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Строка кода
        /// </summary>
        [MaxLength(255)]
        [Required()]
        public string Code { get; set; }
    }
}
