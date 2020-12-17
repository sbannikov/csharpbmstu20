namespace IntermediateAssessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _013 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CodeRows", "Comment", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CodeRows", "Comment");
        }
    }
}
