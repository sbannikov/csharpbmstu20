using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class DB : DbContext
    {
        /// <summary>
        /// База данных Entity Framework Code First
        /// </summary>
        public DB() : base("DB")
        {
            // https://stackoverflow.com/questions/30084916/no-connection-string-named-could-be-found-in-the-application-config-file
        }

        /// <summary>
        /// Автоматическое выполнение миграций
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DB, Migrations.Configuration>());
        }

        /// <summary>
        /// Перечень рубежных контролей
        /// </summary>
        public virtual DbSet<Assessment> Assessments { get; set; }

        /// <summary>
        /// Список студентов
        /// </summary>
        public virtual DbSet<Student> Students { get; set; }

        /// <summary>
        /// Список способностей (РК1N1)
        /// </summary>
        public virtual DbSet<Ability> Abilities { get; set; }

        /// <summary>
        /// Список сотрудников (РК1N1)
        /// </summary>
        public virtual DbSet<Character> Characters { get; set; }

        /// <summary>
        /// Список ролей (РК1N1)
        /// </summary>
        public virtual DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Задания
        /// </summary>
        public virtual DbSet<Exercise> Exercises { get; set; }

        /// <summary>
        /// Задания РК1N1
        /// </summary>
        public virtual DbSet<Exercise1> Exercises1 { get; set; }

        /// <summary>
        /// Задания РК1N2
        /// </summary>
        public virtual DbSet<Exercise2> Exercises2 { get; set; }

        /// <summary>
        /// Задания РК2N1
        /// </summary>
        public virtual DbSet<Exercise3> Exercises3 { get; set; }

        /// <summary>
        /// Строки кода
        /// </summary>
        public virtual DbSet<CodeRow> CodeRows { get; set; }

        /// <summary>
        /// Принципы Agiles
        /// </summary>
        public virtual DbSet<Principle>  Principles { get; set; }
    }
}