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
        /// Номер строки кода, начиная с 1
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Вариант строки кода, начиная с 1
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Строка кода
        /// </summary>
        [MaxLength(255)]
        [Required()]
        public string Code { get; set; }

        /// <summary>
        /// Признак корректности строки кода 
        /// (null, если задание не предполагает наличие корректных и некорректных строк кода)
        /// true - строка корректна
        /// false - строка некорректна
        /// null - задание не предполагает проверки кода, или строка может быть улучшена, но ошибочной не является
        /// </summary>
        public bool? Correct { get; set; }

        /// <summary>
        /// Комментарий к ошибочным строкам
        /// </summary>
        [MaxLength(500)]
        public string Comment { get; set; }

        /// <summary>
        /// Признак корректного ответа
        /// </summary>
        public bool IsCorrect
        {
            get
            {
                return Correct.HasValue && Correct.Value;
            }
        }
    }
}
