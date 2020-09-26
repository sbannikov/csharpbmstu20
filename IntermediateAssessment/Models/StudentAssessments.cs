using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Models
{
    /// <summary>
    /// Студент для выбора рубежного контроля
    /// </summary>
    public class StudentAssessments
    {
        /// <summary>
        /// Студент
        /// </summary>
        public Storage.Student Student { get; set; }

        /// <summary>
        /// Список рубежных контролей
        /// </summary>
        public List<Storage.Assessment> Assessments { get; set; }
    }
}