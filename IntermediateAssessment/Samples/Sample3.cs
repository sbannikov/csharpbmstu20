using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

// Код должен соответствовать требованиям в комментариях
namespace IntermediateAssessment.Samples
{
    public class DataItem
    {
        public string Name;
        public int Number;
    }

    public class DataManager
    {
        // Метод GetName осуществляет поиск в списке по номеру и возвращает имя элемента, соответствующее номеру. 
        public static string GetName(List<DataItem> list, int number)
        {
            DataItem item = list.Where(a => a.Number == number).FirstOrDefault();
            return (item == null) ? null : item.Name;
        }
    }

    // Класс CreditCard предназначен для использования в инфраструктуре Entity Framework в качестве сущности БД. Используется подход Code First. 
    public class CreditCard
    {
        // Уникальный идентификатор записи
        public Guid CardID { get; set; }

        // Наименование кредитной карты
        public string Name { get; private set; }

        // Длительность льготного периода в днях, обязательна для заполнения
        public int GracePeriod { get; set; }

        // Срок действия кредитной карты
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime ExpireDate { get; set; }
    }

    public class Logger
    {
        // Метод должен вернуть полный текст исключения, включая все возможные вложенные исключения. 
        // Текст каждого исключения должен располагаться на новой строке. 
        // Текст предназначен для вывода на веб-странице.
        public static string ExceptionText(Exception ex)
        {
            string m = "";
            do
            {
                m += ex.Message;
                m += @"\n";
                ex = ex.InnerException;
            }
            while (ex.InnerException != null);
            return m;
        }
    }

    public class CreditCardContoller : Controller
    {
        [HttpPost]
        // Создание записи о новой кредитной карте в базе данных
        // Считать, что класс Database - контекст базы данных
        public ActionResult Create([Bind(Include = "Name,GracePeriod,ExpireData")] CreditCard card)
        {
            DateTime today = DateTime.Today;
            if (card.ExpireDate < today)
            {
                ModelState.AddModelError("ExpireDate", "Срок действия карты истёк");
            }
            if (ModelState.IsValid)
            {
                card.CardID = Guid.NewGuid();
                var db = new Storage.DB();
                // db.Cards.Add(card);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}