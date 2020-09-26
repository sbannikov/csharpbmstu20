namespace IntermediateAssessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _003 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Exercise1", "Code", c => c.String(nullable: false, maxLength: 10));
            CreateIndex("dbo.Roles", "Number", unique: true, name: "IX_NUMBER");
            CreateIndex("dbo.Characters", "Number", unique: true, name: "IX_NUMBER");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Characters", "IX_NUMBER");
            DropIndex("dbo.Roles", "IX_NUMBER");
            AlterColumn("dbo.Exercise1", "Code", c => c.String());
        }
    }
}
