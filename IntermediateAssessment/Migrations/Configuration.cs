namespace IntermediateAssessment.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    /// <summary>
    /// Конфигурация миграций
    /// Enable-Migrations -- включение механизма миграций (однократно)
    /// Add-Migration nnn -- добавление очередной миграции
    /// Update-Database -TargetMigration nnn -- переход к нужной миграции
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<IntermediateAssessment.Storage.DB>
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Configuration()
        {
            // Разрешение автоматической миграции
            AutomaticMigrationsEnabled = false;
            // Разрешение миграции с потерей данных
            AutomaticMigrationDataLossAllowed = false;
        }


        /// <summary>
        /// Начальная инициализация базы данных
        /// </summary>
        /// <param name="db">База данных</param>
        protected override void Seed(IntermediateAssessment.Storage.DB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
