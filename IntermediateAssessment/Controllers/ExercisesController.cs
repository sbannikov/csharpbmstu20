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
    public class ExercisesController : DbController
    {
        /// <summary>
        /// Дашборд заданий
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // https://docs.microsoft.com/ru-ru/dotnet/csharp/linq/perform-left-outer-joins
            var list = db.Students.OrderBy(x => x.FileNumber).ToList();
            int width = (int)Math.Round(Math.Sqrt(list.Count() + 0.51));
            var m = new Models.ExercisesMatrix();
            int n = 0;
            while (n < list.Count())
            {
                var il = new List<Models.MatrixItem>();
                for (int i = 0; i < width; i++)
                {
                    var item = new Models.MatrixItem()
                    {
                        Color = list[n].Passed ? "green" : "yellow",
                        FileNumber = list[n].FileNumber
                    };
                    il.Add(item);
                    n++;
                    if (n >= list.Count()) { break; }
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
            var list = db.Students.OrderBy(a => a.LastName).ToList();
            return View(list);
        }
    }
}
