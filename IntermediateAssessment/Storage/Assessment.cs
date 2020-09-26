using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Storage
{
    public class Assessment : Entity
    {
        /// <summary>
        /// Номер рубежного контроля
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Наименование рубежного контроля
        /// </summary>
        public string Name { get; set; }
    }
}