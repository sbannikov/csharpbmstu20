using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntermediateAssessment.Storage
{
    /// <summary>
    /// Задание, выданное конкретному студенту
    /// </summary>
    public class Exercise : Entity
    {
        /// <summary>
        /// Рубежный контроль
        /// </summary>
        [Required()]
        public virtual Assessment Assessment { get; set; }

        /// <summary>
        /// Студент
        /// </summary>
        [Required()]
        public virtual Student Student { get; set; }

        /// <summary>
        /// Время выдачи задания
        /// </summary>
        [DisplayName("Время выдачи РК")]
        [Required()]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Время предоставления задания
        /// </summary>
        [DisplayName("Время сдачи РК на проверку")]
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// Список заданий РК1N1
        /// </summary>
        public virtual HashSet<Exercise1> Exercises1 { get; set; }

        /// <summary>
        /// Список заданий РК1N2
        /// </summary>
        public virtual HashSet<Exercise2> Exercises2 { get; set; }

        /// <summary>
        /// Код версии программного кода в виде последовательности цифр версий
        /// Не более 9 версий на строку кода
        /// </summary>
        [MaxLength(255)]
        public string CodeVersion { get; set; }
        
        /// <summary>
        /// Ответ на задание в текстовой форме
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Exercise()
        {
            // Задание считается выданным сразу после создания
            StartTime = DateTime.Now;
        }
    }
}
