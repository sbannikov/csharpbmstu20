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

        /// <summary>
        /// Административный интерфейс
        /// </summary>
        /// <returns></returns>
        public ActionResult Admin()
        {
            return View();
        }

        /// <summary>
        /// Заглушка
        /// </summary>
        /// <returns></returns>
        public ActionResult No()
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
            // Загрузка исходных данных
            var s = db.Students.Find(sid);
            var a = db.Assessments.Find(aid);
            // Проверка на корректность
            if (a.Number != 1)
            {
                return RedirectToAction("No");
            }
            // Формирование уникального задания
            var e = new Storage.Exercise()
            {
                Student = s,
                Assessment = a
            };

            // Общее количество сотрудников
            int characterCount = db.Characters.Count();

            // Число ролей
            int roleCount = db.Roles.Count();

            // Список сотрудников
            List<int> characters = Utilities.Helper.Randoms(roleCount, characterCount);

            // Формирование вариванта задания для каждой из шести ролей
            for (int n = 1; n <= roleCount; n++)
            {
                // Количество способностей для данной роли
                int aCount = db.Abilities.Where(x => x.Role.Number == n).Count();
                Storage.Exercise1 e1;
                do
                {
                    // Вектор способностей
                    List<int> abilities = Utilities.Helper.Randoms(2, aCount);
                    int a1 = abilities[0];
                    int a2 = abilities[1];

                    // Номер сотрудника
                    int cnumber = characters[n - 1];

                    e1 = new Storage.Exercise1()
                    {
                        Role = db.Roles.Where(x => x.Number == n).First(),
                        Character = db.Characters.Where(x => x.Number == cnumber).First(),
                        Ability1 = db.Abilities.Where(x => x.Role.Number == n && x.Number == a1).First(),
                        Ability2 = db.Abilities.Where(x => x.Role.Number == n && x.Number == a2).First(),
                        Exercise = e
                    };
                    e1.Code = e1.ToString();
                }
                while (db.Exercises1.Where(x => x.Code == e1.Code).FirstOrDefault() != null);
                db.Exercises1.Add(e1);
            }
            // Сохранение уникального задания
            db.Exercises.Add(e);
            db.SaveChanges();

            // Повторное чтение объекта из БД после сохранения
            e = db.Exercises.Find(e.ID);
            return View($"Assessment{a.Number}", e);
        }

        /// <summary>
        /// Проверка и сохранение результатов РК1
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assessment1(Guid id, string[] character)
        {
            // Чтение заданя 
            var e = db.Exercises.Find(id);

            // Проверка первого задания
            foreach (var e1 in e.Exercises1)
            {
                string s = character[e1.Role.Number];
                int n;
                if (int.TryParse(s, out n))
                {
                    e1.CharacterNumber = n;
                    e1.Correct = e1.CharacterNumber.Value == e1.Character.Number;
                }
                else
                {
                    e1.CharacterNumberMessage = "Требуется ввести число";
                    ModelState.AddModelError("character", e1.CharacterNumberMessage);
                }
            }
            if (ModelState.IsValid)
            {
                // Фиксация завершения задания
                e.FinishTime = DateTime.Now;
                db.SaveChanges();

                return View("Assessment1Result", e);
            }
            else
            {
                return View(e);
            }
        }
    }
}