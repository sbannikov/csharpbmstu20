namespace IntermediateAssessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _005 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exercise2",
                c => new
                    {
                    // Задано значение по умолчанию
                    ID = c.Guid(nullable: false, defaultValueSql: "NEWID()"),
                    CodeRow_ID = c.Guid(),
                        Exercise_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CodeRows", t => t.CodeRow_ID)
                // каскадное удаление
                .ForeignKey("dbo.Exercises", t => t.Exercise_ID, cascadeDelete: true)
                .Index(t => t.CodeRow_ID)
                .Index(t => t.Exercise_ID);
            
            AddColumn("dbo.Exercises", "CodeVersion", c => c.String(maxLength: 255));
            AddColumn("dbo.Exercises", "Answer", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exercise2", "Exercise_ID", "dbo.Exercises");
            DropForeignKey("dbo.Exercise2", "CodeRow_ID", "dbo.CodeRows");
            DropIndex("dbo.Exercise2", new[] { "Exercise_ID" });
            DropIndex("dbo.Exercise2", new[] { "CodeRow_ID" });
            DropColumn("dbo.Exercises", "Answer");
            DropColumn("dbo.Exercises", "CodeVersion");
            DropTable("dbo.Exercise2");
        }
    }
}
