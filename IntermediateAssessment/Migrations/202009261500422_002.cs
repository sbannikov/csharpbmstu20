namespace IntermediateAssessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _002 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abilities",
                c => new
                {
                    // Задано значение по умолчанию
                    ID = c.Guid(nullable: false, defaultValueSql: "NEWID()"),
                    RoleID = c.Guid(nullable: false),
                    Number = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 255),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);

            CreateTable(
                "dbo.Roles",
                c => new
                {
                    // Задано значение по умолчанию
                    ID = c.Guid(nullable: false, defaultValueSql: "NEWID()"),
                    Number = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 255),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Characters",
                c => new
                {
                    // Задано значение по умолчанию
                    ID = c.Guid(nullable: false, defaultValueSql: "NEWID()"),
                    Number = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 255),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Exercises",
                c => new
                {
                    // Задано значение по умолчанию
                    ID = c.Guid(nullable: false, defaultValueSql: "NEWID()"),
                    StartTime = c.DateTime(nullable: false),
                    FinishTime = c.DateTime(nullable: true),
                    Assessment_ID = c.Guid(nullable: false),
                    Student_ID = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Assessments", t => t.Assessment_ID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_ID, cascadeDelete: true)
                .Index(t => t.Assessment_ID)
                .Index(t => t.Student_ID);

            CreateTable(
                "dbo.Exercise1",
                c => new
                {
                    // Задано значение по умолчанию
                    ID = c.Guid(nullable: false, defaultValueSql: "NEWID()"),
                    Code = c.String(),
                    Ability1_ID = c.Guid(),
                    Ability2_ID = c.Guid(),
                    Character_ID = c.Guid(),
                    Exercise_ID = c.Guid(),
                    Role_ID = c.Guid(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Abilities", t => t.Ability1_ID)
                .ForeignKey("dbo.Abilities", t => t.Ability2_ID)
                .ForeignKey("dbo.Characters", t => t.Character_ID)
                // каскадное удаление
                .ForeignKey("dbo.Exercises", t => t.Exercise_ID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_ID)
                .Index(t => t.Ability1_ID)
                .Index(t => t.Ability2_ID)
                .Index(t => t.Character_ID)
                .Index(t => t.Exercise_ID)
                .Index(t => t.Role_ID);

            AlterColumn("dbo.Assessments", "Name", c => c.String(nullable: false, maxLength: 255));
        }

        public override void Down()
        {
            DropForeignKey("dbo.Exercises", "Student_ID", "dbo.Students");
            DropForeignKey("dbo.Exercise1", "Role_ID", "dbo.Roles");
            DropForeignKey("dbo.Exercise1", "Exercise_ID", "dbo.Exercises");
            DropForeignKey("dbo.Exercise1", "Character_ID", "dbo.Characters");
            DropForeignKey("dbo.Exercise1", "Ability2_ID", "dbo.Abilities");
            DropForeignKey("dbo.Exercise1", "Ability1_ID", "dbo.Abilities");
            DropForeignKey("dbo.Exercises", "Assessment_ID", "dbo.Assessments");
            DropForeignKey("dbo.Abilities", "RoleID", "dbo.Roles");
            DropIndex("dbo.Exercise1", new[] { "Role_ID" });
            DropIndex("dbo.Exercise1", new[] { "Exercise_ID" });
            DropIndex("dbo.Exercise1", new[] { "Character_ID" });
            DropIndex("dbo.Exercise1", new[] { "Ability2_ID" });
            DropIndex("dbo.Exercise1", new[] { "Ability1_ID" });
            DropIndex("dbo.Exercises", new[] { "Student_ID" });
            DropIndex("dbo.Exercises", new[] { "Assessment_ID" });
            DropIndex("dbo.Abilities", new[] { "RoleID" });
            AlterColumn("dbo.Assessments", "Name", c => c.String());
            DropTable("dbo.Exercise1");
            DropTable("dbo.Exercises");
            DropTable("dbo.Characters");
            DropTable("dbo.Roles");
            DropTable("dbo.Abilities");
        }
    }
}
