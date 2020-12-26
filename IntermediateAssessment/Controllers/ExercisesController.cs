using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IntermediateAssessment.Storage;

namespace IntermediateAssessment.Controllers
{
    /// <summary>
    /// Формирование отчетов
    /// </summary>
    public class ExercisesController : DbController
    {
        /// <summary>
        /// Дашборд заданий
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // https://docs.microsoft.com/ru-ru/dotnet/csharp/linq/perform-left-outer-joins

            // Список всех студентов
            var list = db.Students.OrderBy(x => x.FileNumber).ToList();
            DateTime now = DateTime.Now;
            // Количество доступных РК
            int count = db.Assessments.Where(x => x.StartTime < now).Count();
            int width = (int)Math.Round(Math.Sqrt(count * list.Count() + 0.51));
            var m = new Models.ExercisesMatrix();
            int nstudent = 0;
            int nassessment = 1;
            while (nstudent < list.Count())
            {
                var il = new List<Models.MatrixItem>();
                for (int i = 0; i < width; i++)
                {
                    var item = new Models.MatrixItem(list[nstudent].Exercise(nassessment));
                    il.Add(item);
                    // Следующая ячейка
                    if (++nassessment > count)
                    {
                        nassessment = 1;
                        nstudent++;
                        if (nstudent >= list.Count()) { break; }
                    }
                }
                m.Items.Add(il);
            }
            return View(m);
        }

        /// <summary>
        /// Список студентов
        /// </summary>
        /// <returns></returns>
        public ActionResult Report()
        {
            DateTime now = DateTime.Now;
            // Доступные РК
            var alist = db.Assessments
                .Where(x => x.StartTime < now)
                .OrderBy(x => x.Number).ToList();
            int count = alist.Count();

            // Список групп
            var groups = db.Students.Select(a => a.Group).Distinct()
                .Select(a => new Models.Group() { GroupName = a }).ToList();

            foreach (Models.Group group in groups)
            {
                var students = db.Students.Where(a => a.Group == group.GroupName).OrderBy(a => a.LastName).ToList();
                foreach (var student in students)
                {
                    group.Students.Add(new Models.Student(student, count));
                }
            }

            // Данные для отетча
            var report = new Models.Report(alist, groups);

            // Количество столбцов в отчете: фамилия, имя, группа, и на каждый РК
            ViewBag.ColSpan = 3 + count;

            return View(report);
        }
    }
}
