using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntermediateAssessment.Controllers
{
    public class HomeController : DbController
    {
        /// <summary>
        /// Форма регистрации пользователя
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(new Models.Login());
        }

        public ActionResult Admin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "FileNumber")] Models.Login login)
        {
            string file = login.FileNumber.ToUpper();
            var s = db.Students.FirstOrDefault(a => a.FileNumber == file);
            if (s == null)
            {
                ModelState.AddModelError("FileNumber", "Номер личного дела не найден в списке");
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("Assessments", new { id = s.ID });
            }
            return View(login);
        }

        /// <summary>
        /// Список рубежных контролей
        /// </summary>
        /// <returns></returns>
        public ActionResult Assessments(Guid id)
        {
            var sa = new Models.StudentAssessments();
            sa.Student = db.Students.Find(id);
            sa.Assessments = db.Assessments.OrderBy(a => a.Number).ToList();
            return View(sa);
        }

        /// <summary>
        /// Рубежный контроль - запуск
        /// </summary>
        /// <param name="sid">Идентификатор студента</param>
        /// <param name="aid">Идентификатор рубежного контроля</param>
        /// <returns></returns>
        public ActionResult Assessment(Guid sid, Guid aid)
        {
            var s = db.Students.Find(sid);
            var a = db.Assessments.Find(aid);
            var e = new Storage.Exercise()
            {
                Student = s,
                Assessment = a
            };
            db.Exercises.Add(e);
            db.SaveChanges();
            return View(e);
        }
    }
}