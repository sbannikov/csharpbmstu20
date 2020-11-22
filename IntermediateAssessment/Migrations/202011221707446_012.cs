namespace IntermediateAssessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _012 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CodeRows", "Correct", c => c.Boolean());
            AddColumn("dbo.Exercise2", "AnswerString", c => c.String(maxLength: 255));
            AddColumn("dbo.Exercise2", "Correct", c => c.Boolean());
            AddColumn("dbo.Exercise3", "Correct", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercise3", "Correct");
            DropColumn("dbo.Exercise2", "Correct");
            DropColumn("dbo.Exercise2", "AnswerString");
            DropColumn("dbo.CodeRows", "Correct");
        }
    }
}
