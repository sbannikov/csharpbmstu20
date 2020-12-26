using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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
        /// Идентификатор студента
        /// </summary>
        [Column("Student_ID")]
        public Guid StudentID { get; set; }

        /// <summary>
        /// Студент
        /// </summary>
        [Required()]
        [ForeignKey("StudentID")]
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
        /// Список заданий РК2N1
        /// </summary>
        public virtual HashSet<Exercise3> Exercises3 { get; set; }

        /// <summary>
        /// Код первого задания
        /// РК1 - не используется
        /// РК2 - 12 шестнадцатеричных цифр
        /// </summary>
        [MaxLength(12)]
        public string Code { get; set; }

        /// <summary>
        /// Уникальный код второго задания
        /// Код версии программного кода в виде последовательности цифр версий
        /// Не более 9 версий на строку кода
        /// </summary>
        [MaxLength(255)]
        public string CodeVersion { get; set; }
        
        /// <summary>
        /// Ответ на задание в текстовой форме
        /// </summary>
        [AllowHtml()]
        public string Answer { get; set; }

        /// <summary>
        /// Платформа (операционная система) пользователя
        /// </summary>
        [MaxLength(255)]
        [Required()]
        public string UserPlatform { get; set; }

        /// <summary>
        /// Браузер пользователя
        /// </summary>
        [MaxLength(255)]
        [Required()]
        public string UserBrowser { get; set; }

        /// <summary>
        /// Адрес пользователя
        /// </summary>
        [MaxLength(255)]
        [Required()]
        public string UserAddress { get; set; }

        /// <summary>
        /// Сетевой узел пользователя
        /// </summary>
        [MaxLength(255)]
        [Required()]
        public string UserHost { get; set; }

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Exercise()
        {
            // Задание считается выданным сразу после создания
            StartTime = DateTime.Now;
        }

        /// <summary>
        /// Признак сдачи (завершения выполнения) задания
        /// </summary>
        public bool Passed
        {
            get
            {
                return FinishTime.HasValue;
            }
        }
    }
}
