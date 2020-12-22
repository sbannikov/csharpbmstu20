namespace IntermediateAssessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _014 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assessments", "ShortName", c => c.String(nullable: false, maxLength: 4, defaultValue: ""));
        }

        public override void Down()
        {
            DropColumn("dbo.Assessments", "ShortName");
        }
    }
}
