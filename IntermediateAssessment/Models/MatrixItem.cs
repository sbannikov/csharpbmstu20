using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Models
{
    /// <summary>
    /// Элемент матрицы статистики
    /// </summary>
    public class MatrixItem
    {
        /// <summary>
        /// Ссылка на выполненное задание
        /// </summary>
        public Storage.Exercise Exercise { get; private set; }

        /// <summary>
        /// Конструктор по заданию
        /// </summary>
        /// <param name="exercise">Задание</param>
        public MatrixItem(Storage.Exercise exercise)
        {
            Exercise = exercise;
        }

        /// <summary>
        /// Задание сдано
        /// </summary>
        public bool Passed
        {
            get
            {
                return (Exercise != null) ? Exercise.Passed : false;
            }
        }

        /// <summary>
        /// Отображаемый цвет в матрице
        /// </summary>
        public string Color
        {
            get
            {
                return (Exercise != null) ? (Exercise.Passed ? "lightgreen" : "lightyellow") : "lightgrey";
            }
        }

        /// <summary>
        /// Состояния задания
        /// </summary>
        public string Status
        {
            get
            {
                return (Exercise != null) ? (Exercise.Passed ? "сдан" : "в процессе") : Storage.Student.Dash;
            }
        }
    }
}