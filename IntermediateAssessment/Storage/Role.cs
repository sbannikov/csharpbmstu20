using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntermediateAssessment.Storage
{
    /// <summary>
    /// Проектная роль
    /// </summary>
    public class Role : Entity
    {
        /// <summary>
        /// Номер роли (от 1 до 6)
        /// </summary>
        [DisplayName("Номер роли")]
        [Index("IX_NUMBER", IsUnique = true)]
        [Range(1, 6)]
        public int Number { get; set; }

        /// <summary>
        /// Наименование проектной роли
        /// </summary>
        [DisplayName("Наименование")]
        [MaxLength(255)]
        [Required()]
        public string Name { get; set; }

    }
}
