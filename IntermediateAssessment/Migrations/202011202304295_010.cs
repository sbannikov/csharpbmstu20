namespace IntermediateAssessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _010 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Exercise1", "Exercise_ID", "dbo.Exercises");
            DropForeignKey("dbo.Exercise2", "Exercise_ID", "dbo.Exercises");
            DropIndex("dbo.Exercise1", new[] { "Exercise_ID" });
            DropIndex("dbo.Exercise2", new[] { "Exercise_ID" });
            CreateTable(
                "dbo.Exercise3",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Number = c.Int(nullable: false),
                        Exercise_ID = c.Guid(nullable: false),
                        Principle_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Exercises", t => t.Exercise_ID, cascadeDelete: true)
                .ForeignKey("dbo.Principles", t => t.Principle_ID, cascadeDelete: true)
                .Index(t => t.Exercise_ID)
                .Index(t => t.Principle_ID);
            
            AddColumn("dbo.Exercises", "Code", c => c.String(maxLength: 12));
            AlterColumn("dbo.Exercise1", "Exercise_ID", c => c.Guid(nullable: false));
            AlterColumn("dbo.Exercise2", "Exercise_ID", c => c.Guid(nullable: false));
            CreateIndex("dbo.Exercise1", "Exercise_ID");
            CreateIndex("dbo.Exercise2", "Exercise_ID");
            AddForeignKey("dbo.Exercise1", "Exercise_ID", "dbo.Exercises", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Exercise2", "Exercise_ID", "dbo.Exercises", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exercise2", "Exercise_ID", "dbo.Exercises");
            DropForeignKey("dbo.Exercise1", "Exercise_ID", "dbo.Exercises");
            DropForeignKey("dbo.Exercise3", "Principle_ID", "dbo.Principles");
            DropForeignKey("dbo.Exercise3", "Exercise_ID", "dbo.Exercises");
            DropIndex("dbo.Exercise3", new[] { "Principle_ID" });
            DropIndex("dbo.Exercise3", new[] { "Exercise_ID" });
            DropIndex("dbo.Exercise2", new[] { "Exercise_ID" });
            DropIndex("dbo.Exercise1", new[] { "Exercise_ID" });
            AlterColumn("dbo.Exercise2", "Exercise_ID", c => c.Guid());
            AlterColumn("dbo.Exercise1", "Exercise_ID", c => c.Guid());
            DropColumn("dbo.Exercises", "Code");
            DropTable("dbo.Exercise3");
            CreateIndex("dbo.Exercise2", "Exercise_ID");
            CreateIndex("dbo.Exercise1", "Exercise_ID");
            AddForeignKey("dbo.Exercise2", "Exercise_ID", "dbo.Exercises", "ID");
            AddForeignKey("dbo.Exercise1", "Exercise_ID", "dbo.Exercises", "ID");
        }
    }
}
