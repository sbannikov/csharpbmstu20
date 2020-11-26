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
    /// Контроллер с базой данных
    /// </summary>
    [Filters.Logging()]
    public abstract class DbController : Controller
    {
        /// <summary>
        /// Генератор случайных чисел
        /// </summary>
        protected static Random rnd = new Random();

        /// <summary>
        /// База данных
        /// </summary>
        protected DB db = new DB();

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}