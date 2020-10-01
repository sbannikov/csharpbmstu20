namespace IntermediateAssessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _006 : DbMigration
    {
        public override void Up()
        {
            // Задано значение по умолчанию
            AddColumn("dbo.Assessments", "StartTime", c => c.DateTime(nullable: false, defaultValue: new DateTime(2049,01,01)));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assessments", "StartTime");
        }
    }
}
