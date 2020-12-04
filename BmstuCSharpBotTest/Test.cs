using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BmstuCSharpBot.Tests
{
    /// <summary>
    /// Автоматические тесты
    /// </summary>
    [TestFixture()]
    public class Test
    {
        [Test()]
        public void Test1()
        {
            var user = new User();
            Assert.IsFalse(user.IsFull);
        }

        [Test()]
        public void Test2()
        {
            var user = new User()
            {
                Phone = "+7(495)555-55-55"
            };
            Assert.IsFalse(user.IsFull);
        }

        [Test()]
        public void Test3()
        {
            var user = new User()
            {
                Email = "user@example.com"
            };
            Assert.IsFalse(user.IsFull);
        }

        [Test()]
        public void Test4()
        {
            var user = new User()
            {
                Phone = "+7(495)555-55-55",
                Email = "user@example.com"
            };
            Assert.IsTrue(user.IsFull);
        }

        [Test()]
        public void Test5()
        {
            User user = null;

            Assume.That(user != null, "Переменная user должна быть инициализиована");
            Assert.IsTrue(user.IsFull);
        }

        [TestCaseSource("GetUsers")]
        public void Test6(UserTest user)
        {
            Assume.That(user.User != null, "Переменная user должна быть инициализиована");
            // Проверка на выполнение условия
            Assert.IsTrue(user.IsFull == user.User.IsFull);
            // Альтернативный вариант проверкиы
            Assert.AreEqual(user.IsFull, user.User.IsFull);
        }

        /// <summary>
        /// Список тестовых данных
        /// </summary>
        /// <returns></returns>
        public static List<UserTest> GetUsers()
        {
            var list = new List<UserTest>();
            UserTest item;

            item = new UserTest()
            {
                User = new User(),
                IsFull = false
            };
            list.Add(item);

            item = new UserTest()
            {
                User = new User()
                {
                    Phone = "+7(495)555-55-55"
                },
                IsFull = false
            };
            list.Add(item);

            item = new UserTest()
            {
                User = new User()
                {
                    Email = "user@example.com"
                },
                IsFull = false
            };
            list.Add(item);

            item = new UserTest()
            {
                User = new User()
                {
                    Phone = "+7(812)555-55-55",
                    Email = "user@example.com"
                },
                IsFull = true
            };
            list.Add(item);

            item = new UserTest()
            {
                User = null,
                IsFull = false
            };
            list.Add(item);

            return list;
        }
    }
}
