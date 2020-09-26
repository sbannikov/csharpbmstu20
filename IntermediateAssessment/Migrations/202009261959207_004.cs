namespace IntermediateAssessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _004 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CodeRows",
                c => new
                {
                    // Задано значение по умолчанию
                    ID = c.Guid(nullable: false, defaultValueSql: "NEWID()"),
                    Row = c.Int(nullable: false),
                    Version = c.Int(nullable: false),
                    Code = c.String(nullable: false, maxLength: 255),
                    Assessment_ID = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Assessments", t => t.Assessment_ID, cascadeDelete: true)
                .Index(t => t.Assessment_ID);

            AddColumn("dbo.Exercise1", "CharacterNumber", c => c.Int());
            AddColumn("dbo.Exercise1", "Correct", c => c.Boolean());
        }

        public override void Down()
        {
            DropForeignKey("dbo.CodeRows", "Assessment_ID", "dbo.Assessments");
            DropIndex("dbo.CodeRows", new[] { "Assessment_ID" });
            DropColumn("dbo.Exercise1", "Correct");
            DropColumn("dbo.Exercise1", "CharacterNumber");
            DropTable("dbo.CodeRows");
        }
    }
}
