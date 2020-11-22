namespace IntermediateAssessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _011 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercise3", "AnswerNumber", c => c.Int());
            // Переименование столбца вместо пересоздания
            RenameColumn("dbo.Exercise1", "CharacterNumber", "AnswerNumber");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercise3", "AnswerNumber");
            // Переименование столбца вместо пересоздания
            RenameColumn("dbo.Exercise1", "AnswerNumber", "CharacterNumber");
        }
    }
}
