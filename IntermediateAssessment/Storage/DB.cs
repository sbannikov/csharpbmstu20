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
        public virtual DbSet<Student>  Students{ get; set; }
    }
}