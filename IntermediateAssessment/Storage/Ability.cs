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
    /// Способность (качество) сотрудника
    /// </summary>
    public class Ability : Entity
    {
        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public Guid RoleID { get; set; }

        /// <summary>
        /// Проектная роль, для которой данное качество необходимо
        /// </summary>
        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }

        /// <summary>
        /// Номер способности
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Способность
        /// </summary>
        [DisplayName("Способность")]
        [MaxLength(255)]
        [Required()]
        public string Name { get; set; }

    }
}
