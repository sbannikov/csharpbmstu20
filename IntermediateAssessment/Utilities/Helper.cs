using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntermediateAssessment.Utilities
{
    /// <summary>
    /// Набор вспомогательных функций
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Генератор случайных чисел
        /// </summary>
        private static Random rnd = new Random();

        /// <summary>
        /// Генерация вектора неповторяющихся случайных целых чисел заданной длины
        /// </summary>
        /// <param name="count">Количество чисел</param>
        /// <param name="max">Максимальное число</param>
        /// <returns></returns>
        public static List<int> Randoms(int count, int max)
        {
            List<int> list = new List<int>();
            for (int i = 1; i <= count; i++)
            {
                int n;
                do
                {
                    n = rnd.Next(1, max + 1);
                }
                while (list.Contains(n));
                list.Add(n);
            }

            return list;
        }

        /// <summary>
        /// Генерация случайного числа от 1 до max, которого нет в списке list
        /// </summary>
        /// <param name="list">Список целых чисел</param>
        /// <param name="max">Максимальное число</param>
        /// <returns></returns>
        public static int UniqueRandom(List<int> list, int max)
        {
            int n;
            do
            {
                n = rnd.Next(1, max + 1);
            }
            while (list.Contains(n));
            return n;
        }
    }
}
