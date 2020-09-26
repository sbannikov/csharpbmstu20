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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "FileNumber")] Models.Login login)
        {
            var s = db.Students.FirstOrDefault(a => a.FileNumber == login.FileNumber);
            if (s == null)
            {
                ModelState.AddModelError("FileNumber", "Номер личного дела не найден в списке");
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(login);
        }
    }
}