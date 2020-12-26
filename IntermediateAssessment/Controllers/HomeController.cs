using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace IntermediateAssessment.Controllers
{
    /// <summary>
    /// Формирование и фиксирование заданий
    /// </summary>
    public class HomeController : DbController
    {
        /// <summary>
        /// Количество ценностей Agile
        /// </summary>
        private const int ValueCount = 4;

        /// <summary>
        /// Количество времени на рубежный контроль в минутах
        /// </summary>
        private const int MaxMinutes = 91;

        /// <summary>
        /// Протоколирование журнала событий
        /// </summary>
        private static Logger log = LogManager.GetCurrentClassLogger();

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
        public ActionResult Index([Bind(Include = "FileNumber")] Models.Login login)
        {
            try
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
            catch (Exception ex)
            {
                log.Warn(ex);
                return View(login);
            }
        }

        /// <summary>
        /// Список рубежных контролей
        /// </summary>
        /// <returns></returns>
        public ActionResult Assessments(Guid id)
        {
            try
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
            catch (Exception ex)
            {
                log.Warn(ex);
                return View("Message", (object)"Внутренняя ошибка");
            }
        }

        /// <summary>
        /// РК 1 Задание 1 - формирование
        /// </summary>
        /// <param name="e"></param>
        private void Exercise1(Storage.Exercise e)
        {
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
        }

        /// <summary>
        /// РК1 Задание 2 - Формирование
        /// </summary>
        /// <param name="e">Уникальное задание</param>
        private void Exercise2(Storage.Exercise e)
        {
            Guid aid = e.Assessment.ID; // Идентификатор РК
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
            // Сохранение уникального кода задания
            e.CodeVersion = codever;
            db.Exercises2.AddRange(list2);
        }

        /// <summary>
        /// РК2 Задание 1 - Формирование
        /// </summary>
        /// <param name="e"></param>
        private void Exercise3(Storage.Exercise e)
        {
            // Количество принципов
            int principles = db.Principles.Count();

            string code; // Код задания
            List<int> list; // Вектор номеров принципов

            // Формирование уникальной комбинации принципов Agile
            do
            {
                list = Utilities.Helper.Randoms(principles, principles);
                code = string.Join("", list.Select(a => a.ToString("X1")));
            }
            while (db.Exercises.Where(x => x.Code == code).Any());

            // Формирование задания
            for (int i = 0; i < principles; i++)
            {
                int n = list[i];
                // Считаем, что принцип в базе существует
                var p = db.Principles.Where(a => a.Number == n).First();
                var item = new Storage.Exercise3()
                {
                    Exercise = e,
                    Principle = p,
                    Number = i + 1 // Нумерацию начинаем с 1
                };
                db.Exercises3.Add(item);
            }
            // Сохранение уникального кода
            e.Code = code;
        }

        /// <summary>
        /// Рубежный контроль - запуск
        /// </summary>
        /// <param name="sid">Идентификатор студента</param>
        /// <param name="aid">Идентификатор рубежного контроля</param>
        /// <returns></returns>
        public ActionResult Assessment(Guid sid, Guid aid)
        {
            try
            {
                DateTime now = DateTime.Now;

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

                Storage.Exercise e;

                // Проверка на повторный запуск
                e = db.Exercises.Where
                    (x => (x.Student.ID == sid) && (x.Assessment.ID == aid) && x.FinishTime.HasValue).FirstOrDefault();

                // Есть завершенный РК
                if (e != null)
                {
                    // Повторная сдача не допускается
                    return View($"Assessment{a.Number}Result", e);
                }

                // Проверка на идущий рубежный контроль
                e = db.Exercises
                    .Where(x => (x.Student.ID == sid) && (x.Assessment.ID == aid) && !x.FinishTime.HasValue)
                    .ToList() // для выделения запроса сервер/клиент
                    .Where(x => (now - x.StartTime).TotalMinutes < MaxMinutes)
                    .OrderByDescending(x => x.StartTime)
                    .FirstOrDefault();

                if (e == null)
                {
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

                    switch (a.Number)
                    {
                        case 1:
                            Exercise1(e);
                            Exercise2(e);
                            break;

                        case 2:
                            Exercise3(e);
                            Exercise2(e);
                            break;

                        case 3:
                            Exercise2(e);
                            break;

                        case 4: // Домашнее задание
                            break;

                        default:
                            return View("Message", (object)"Некорректный номер РК");
                    }

                    // Сохранение уникального задания
                    db.Exercises.Add(e);
                    db.SaveChanges();

                    // Повторное чтение объекта из БД после сохранения
                    e = db.Exercises.Find(e.ID);
                }

                // Время окончания приёма задания
                ViewBag.FinishTime = e.StartTime.AddMinutes(MaxMinutes);

                return View($"Assessment{a.Number}", e);
            }
            catch (Exception ex)
            {
                log.Warn(ex);
                return View("Message", (object)"Внутренняя ошибка");
            }
        }

        /// <summary>
        /// Проверка и сохранение результатов РК1
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Assessment1(Guid id, string[] answer, string xml)
        {
            try
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
                    string s = answer[e1.Role.Number];
                    int n;
                    if (int.TryParse(s, out n))
                    {
                        e1.AnswerNumber = n;
                        e1.Correct = e1.AnswerNumber.Value == e1.Character.Number;
                    }
                    else
                    {
                        e1.AnswerNumberMessage = "Требуется ввести число";
                        ModelState.AddModelError("answer", e1.AnswerNumberMessage);
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
            catch (Exception ex)
            {
                log.Warn(ex);
                return View("Message", (object)"Внутренняя ошибка");
            }
        }

        /// <summary>
        /// Проверка и сохранение результатов РК2
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Assessment2(Guid id, string[] answer, string[] code, string text)
        {
            try
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
                foreach (var e3 in e.Exercises3.OrderBy(x => x.Number).ToList())
                {
                    string s = answer[e3.Number];
                    int n;
                    if (int.TryParse(s, out n))
                    {
                        if ((n >= 1) && (n <= ValueCount))
                        {
                            e3.AnswerNumber = n;
                        }
                        else
                        {
                            e3.AnswerNumberMessage = $"Требуется ввести число от 1 до {ValueCount}";
                            ModelState.AddModelError("answer", e3.AnswerNumberMessage);
                        }
                    }
                    else
                    {
                        e3.AnswerNumberMessage = $"Требуется ввести число от 1 до {ValueCount}";
                        ModelState.AddModelError("answer", e3.AnswerNumberMessage);
                    }
                }

                // Проверка второго задания
                foreach (var e2 in e.Exercises2.OrderBy(x => x.CodeRow.Row).ToList())
                {
                    // Сохраним данный ответ
                    e2.AnswerString = code[e2.CodeRow.Row - 1];
                    // Нормализация ответа для анализа
                    string s = e2.AnswerString.Trim();
                    // Если в задании строка кода корректная
                    if (e2.CodeRow.IsCorrect)
                    {
                        // То если ее не исправили - всё хорошо, иначе это ошибка
                        // (не чини то, что не сломалось)
                        e2.Correct = string.IsNullOrEmpty(s);
                        continue;
                    }

                    // Определение вариантов ответов
                    var row = db.CodeRows.Where(x => x.Assessment.ID == e.Assessment.ID && x.Row == e2.CodeRow.Row && x.Code.Trim() == s).FirstOrDefault();
                    if (row != null)
                    {
                        // Вариант ответа найден - корректность известна
                        e2.Correct = row.Correct;
                    }
                    else
                    {
                        e2.Correct = null; // Система не может принять решение о корректности ответа
                    }
                }
                if (ModelState.IsValid)
                {
                    // Ответ в произвольном виде
                    e.Answer = text;
                    // Фиксация завершения задания
                    db.SaveChanges();

                    return View("Assessment2Result", e);
                }
                else
                {
                    return View(e);
                }
            }
            catch (Exception ex)
            {
                log.Warn(ex);
                return View("Message", (object)"Внутренняя ошибка");
            }
        }

        /// <summary>
        /// Проверка и сохранение результатов РК3
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Assessment3(Guid id, string[] code, string text)
        {
            try
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

                // Проверка второго задания
                foreach (var e2 in e.Exercises2.OrderBy(x => x.CodeRow.Row).ToList())
                {
                    // Сохраним данный ответ
                    e2.AnswerString = code[e2.CodeRow.Row - 1];
                    // Нормализация ответа для анализа
                    string s = e2.AnswerString.Trim();
                    // Если в задании строка кода корректная
                    if (e2.CodeRow.IsCorrect)
                    {
                        // То если ее не исправили - всё хорошо, иначе это ошибка
                        // (не чини то, что не сломалось)
                        e2.Correct = string.IsNullOrEmpty(s);
                        continue;
                    }

                    // Если некоррекная строка не исправлена
                    // (считаем, что исправление всегда не пусто)
                    if (string.IsNullOrEmpty(s))
                    {
                        // Значит, ответ неверный
                        e2.Correct = false;
                        continue;
                    }

                    // Определение вариантов ответов
                    var row = db.CodeRows.Where(x => x.Assessment.ID == e.Assessment.ID && x.Row == e2.CodeRow.Row && x.Code.Trim() == s).FirstOrDefault();
                    if (row != null)
                    {
                        // Вариант ответа найден - корректность известна
                        e2.Correct = row.Correct;
                    }
                    else
                    {
                        e2.Correct = null; // Система не может принять решение о корректности ответа
                    }
                }
                if (ModelState.IsValid)
                {
                    // Ответ в произвольном виде
                    e.Answer = text;
                    // Фиксация завершения задания
                    db.SaveChanges();

                    return View("Assessment3Result", e);
                }
                else
                {
                    return View(e);
                }
            }
            catch (Exception ex)
            {
                log.Warn(ex);
                return View("Message", (object)"Внутренняя ошибка");
            }
        }

        /// <summary>
        /// Cохранение результатов ДЗ
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Assessment4(Guid id, string url)
        {
            try
            {
                // Чтение задания 
                var e = db.Exercises.Find(id);
                if (e == null)
                {
                    return View("Message", (object)"Некорректный идентификатор объекта");
                }
                // Сохранение времени финиша
                e.FinishTime = DateTime.Now;

                if (ModelState.IsValid)
                {
                    // Ответ в произвольном виде
                    e.Answer = url;
                    // Фиксация завершения задания
                    db.SaveChanges();

                    return View("Assessment4Result", e);
                }
                else
                {
                    return View(e);
                }
            }
            catch (Exception ex)
            {
                log.Warn(ex);
                return View("Message", (object)"Внутренняя ошибка");
            }
        }

        /// <summary>
        /// Загрузка результата выполнения задания
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Exercise(Guid id)
        {
            try
            {
                // Чтение задания 
                var e = db.Exercises.Find(id);
                if (e == null)
                {
                    return View("Message", (object)"Некорректный идентификатор объекта");
                }
                return View($"Assessment{e.Assessment.Number}Result", e); 
            }
            catch (Exception ex)
            {
                log.Warn(ex);
                return View("Message", (object)"Внутренняя ошибка");
            }
        }
    }
}