﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntermediateAssessment.Controllers
{
    public class HomeController : DbController
    {
        /// <summary>
        /// Количество времени на рубежный контроль в минутах
        /// </summary>
        private const int MaxMinutes = 91;

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

        /// <summary>
        /// Текстовое сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns></returns>
        public ActionResult Message(string message)
        {
            return View((object)message);
        }

        /// <summary>
        /// Аутентификация студента по номеру личного дела
        /// </summary>
        /// <param name="login">Имя личного дела</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "FileNumber")] Models.Login login)
        {
            // Отсекаем пробелы на всякий случай
            string file = login.FileNumber?.ToUpper().Trim();
            var s = db.Students.FirstOrDefault(a => a.FileNumber == file);
            if (s == null)
            {
                ModelState.AddModelError("FileNumber", "Номер личного дела не найден в списке. Для регистрации на курс требуется заполнить анкету");
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
            var sa = new Models.StudentAssessments()
            {
                Student = db.Students.Find(id),
                Assessments = db.Assessments.OrderBy(a => a.Number).ToList()
            };
            if (sa.Student == null)
            {
                return View("Message", (object)"Некорректный идентификатор объекта");
            }
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
            // Проверка на корректность параметров
            if ((s == null) || (a == null))
            {
                return View("Message", (object)"Некорректный идентификатор объекта");
            }
            // Проверка на доступное время
            if (a.StartTime > DateTime.Now)
            {
                return View("Message", (object)$"{a.Name} начнётся {a.StartTime:dd-MM-yyyy} в {a.StartTime:HH:mm}");
            }
            // Проверка на повторный запуск
            Storage.Exercise e = db.Exercises.Where
                (x => (x.Student.ID == sid) && (x.Assessment.ID == aid) && x.FinishTime.HasValue).FirstOrDefault();

            // Есть завершенный РК
            if (e != null)
            {
                // Повторная сдача не допускается
                return View($"Assessment{a.Number}Result", e);
            }

            // Формирование уникального задания
            e = new Storage.Exercise()
            {
                Student = s,
                Assessment = a,
                // Сведения о клиенте
                UserAddress = Request.UserHostAddress,
                UserBrowser = Request.Browser.Browser,
                UserHost = Request.UserHostName,
                UserPlatform = Request.Browser.Platform
            };

            // Формирование РК1

            // Задание 1 РК1

            // Общее количество сотрудников
            int characterCount = db.Characters.Count();

            // Список заданий
            var elist = new List<Storage.Exercise1>();

            // Формирование варианта задания для каждой из шести ролей
            foreach (var role in db.Roles.OrderBy(x => x.Number).ToList())
            {
                // Количество способностей для данной роли
                int aCount = db.Abilities.Where(x => x.Role.Number == role.Number).Count();
                Storage.Exercise1 e1;
                do
                {
                    // Вектор способностей
                    List<int> abilities = Utilities.Helper.Randoms(2, aCount);
                    int a1 = abilities[0];
                    int a2 = abilities[1];

                    // Номер уникального сотрудника
                    int cnumber = Utilities.Helper.UniqueRandom(elist.Select(x => x.Character.Number).ToList(), characterCount);

                    e1 = new Storage.Exercise1()
                    {
                        Role = role,
                        Character = db.Characters.Where(x => x.Number == cnumber).First(),
                        Ability1 = db.Abilities.Where(x => x.Role.Number == role.Number && x.Number == a1).First(),
                        Ability2 = db.Abilities.Where(x => x.Role.Number == role.Number && x.Number == a2).First(),
                        Exercise = e
                    };
                    e1.Code = e1.ToString();
                }
                while (db.Exercises1.Where(x => x.Code == e1.Code).FirstOrDefault() != null);
                elist.Add(e1);
            }
            db.Exercises1.AddRange(elist);

            // Задание 2 РК1
            string codever;
            List<Storage.Exercise2> list2;
            do
            {
                codever = string.Empty;
                list2 = new List<Storage.Exercise2>();
                // Список всех строк кода данного РК
                foreach (int row in db.CodeRows.Where(x => x.Assessment.ID == aid).Select(x => x.Row).Distinct())
                {
                    // Определение количества вариантов строки
                    int versions = db.CodeRows.Where(x => (x.Assessment.ID) == aid && (x.Row == row)).Count();
                    // Случайный выбор варианта
                    int version = (versions == 1) ? 1 : rnd.Next(1, versions + 1);
                    // Загрузка варианта
                    var code = db.CodeRows.Where(x => (x.Assessment.ID) == aid && (x.Row == row) && (x.Version == version)).First();
                    // Ключ варианта
                    codever += version;
                    // Сохранение варианта
                    list2.Add(new Storage.Exercise2()
                    {
                        Exercise = e,
                        CodeRow = code
                    });
                }
            }
            while (db.Exercises.Where(x => x.CodeVersion == codever).FirstOrDefault() != null);
            db.Exercises2.AddRange(list2);

            // Сохранение уникального задания
            db.Exercises.Add(e);
            db.SaveChanges();

            // Повторное чтение объекта из БД после сохранения
            e = db.Exercises.Find(e.ID);

            // Время окончания приёма задания
            ViewBag.FinishTime = e.StartTime.AddMinutes(MaxMinutes);

            return View($"Assessment{a.Number}", e);
        }

        /// <summary>
        /// Проверка и сохранение результатов РК1
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Assessment1(Guid id, string[] character, string xml)
        {
            // Чтение задания 
            var e = db.Exercises.Find(id);
            if (e == null)
            {
                return View("Message", (object)"Некорректный идентификатор объекта");
            }

            // Проверка на превышение времени
            e.FinishTime = DateTime.Now;
            if ((e.FinishTime.Value - e.StartTime).TotalMinutes > MaxMinutes)
            {
                return View("OutOfTime", e);
            }

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
                // Фиксация ответа в свободной форме (задание 2)
                e.Answer = xml;
                // Фиксация завершения задания
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