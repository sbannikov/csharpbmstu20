namespace IntermediateAssessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _007 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "UserPlatform", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Exercises", "UserBrowser", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Exercises", "UserAddress", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Exercises", "UserHost", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "UserHost");
            DropColumn("dbo.Exercises", "UserAddress");
            DropColumn("dbo.Exercises", "UserBrowser");
            DropColumn("dbo.Exercises", "UserPlatform");
        }
    }
}
