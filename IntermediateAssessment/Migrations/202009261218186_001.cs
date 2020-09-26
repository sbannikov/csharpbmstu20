namespace IntermediateAssessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assessments",
                c => new
                {
                    // Заданое значение по умолчнанию
                    ID = c.Guid(nullable: false, defaultValueSql: "NEWID()"),
                    Number = c.Int(nullable: false),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Students",
                c => new
                {
                    // Заданое значение по умолчнанию
                    ID = c.Guid(nullable: false, defaultValueSql: "NEWID()"),
                    FirstName = c.String(nullable: false, maxLength: 255),
                    LastName = c.String(nullable: false, maxLength: 255),
                    Group = c.String(nullable: false, maxLength: 16),
                    FileNumber = c.String(nullable: false, maxLength: 16),
                })
                .PrimaryKey(t => t.ID);

        }

        public override void Down()
        {
            DropTable("dbo.Students");
            DropTable("dbo.Assessments");
        }
    }
}
