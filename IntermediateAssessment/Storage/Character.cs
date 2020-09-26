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
    /// Участник проектной команды
    /// </summary>
    public class Character : Entity
    {
        /// <summary>
        /// Номер 
        /// </summary>       
        [DisplayName("Табельный номер")]
        [Index("IX_NUMBER", IsUnique = true)]
        public int Number { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [MaxLength(255)]
        [Required()]
        [DisplayName("Имя сотрудника")]
        public string Name { get; set; }
    }
}
