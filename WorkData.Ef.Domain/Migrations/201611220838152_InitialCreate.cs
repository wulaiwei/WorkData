namespace WorkData.EF.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EF_CONTENT_DESCRIPTION_FIELD", "CONTENT_ID", "dbo.EF_CONTENT");
            DropForeignKey("dbo.EF_CONTENT_DOUBLE_FIELD", "CONTENT_ID", "dbo.EF_CONTENT");
            DropForeignKey("dbo.EF_CONTENT_INT_FIELD", "CONTENT_ID", "dbo.EF_CONTENT");
            DropForeignKey("dbo.EF_CONTENT_STRING_FIELD", "CONTENT_ID", "dbo.EF_CONTENT");
            DropForeignKey("dbo.EF_CONTENT_TEXT_FIELD", "CONTENT_ID", "dbo.EF_CONTENT");
            DropForeignKey("dbo.EF_CONTENT_TIME_FIELD", "CONTENT_ID", "dbo.EF_CONTENT");
            AddForeignKey("dbo.EF_CONTENT_DESCRIPTION_FIELD", "CONTENT_ID", "dbo.EF_CONTENT", "CONTENT_ID", cascadeDelete: true);
            AddForeignKey("dbo.EF_CONTENT_DOUBLE_FIELD", "CONTENT_ID", "dbo.EF_CONTENT", "CONTENT_ID", cascadeDelete: true);
            AddForeignKey("dbo.EF_CONTENT_INT_FIELD", "CONTENT_ID", "dbo.EF_CONTENT", "CONTENT_ID", cascadeDelete: true);
            AddForeignKey("dbo.EF_CONTENT_STRING_FIELD", "CONTENT_ID", "dbo.EF_CONTENT", "CONTENT_ID", cascadeDelete: true);
            AddForeignKey("dbo.EF_CONTENT_TEXT_FIELD", "CONTENT_ID", "dbo.EF_CONTENT", "CONTENT_ID", cascadeDelete: true);
            AddForeignKey("dbo.EF_CONTENT_TIME_FIELD", "CONTENT_ID", "dbo.EF_CONTENT", "CONTENT_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EF_CONTENT_TIME_FIELD", "CONTENT_ID", "dbo.EF_CONTENT");
            DropForeignKey("dbo.EF_CONTENT_TEXT_FIELD", "CONTENT_ID", "dbo.EF_CONTENT");
            DropForeignKey("dbo.EF_CONTENT_STRING_FIELD", "CONTENT_ID", "dbo.EF_CONTENT");
            DropForeignKey("dbo.EF_CONTENT_INT_FIELD", "CONTENT_ID", "dbo.EF_CONTENT");
            DropForeignKey("dbo.EF_CONTENT_DOUBLE_FIELD", "CONTENT_ID", "dbo.EF_CONTENT");
            DropForeignKey("dbo.EF_CONTENT_DESCRIPTION_FIELD", "CONTENT_ID", "dbo.EF_CONTENT");
            AddForeignKey("dbo.EF_CONTENT_TIME_FIELD", "CONTENT_ID", "dbo.EF_CONTENT", "CONTENT_ID");
            AddForeignKey("dbo.EF_CONTENT_TEXT_FIELD", "CONTENT_ID", "dbo.EF_CONTENT", "CONTENT_ID");
            AddForeignKey("dbo.EF_CONTENT_STRING_FIELD", "CONTENT_ID", "dbo.EF_CONTENT", "CONTENT_ID");
            AddForeignKey("dbo.EF_CONTENT_INT_FIELD", "CONTENT_ID", "dbo.EF_CONTENT", "CONTENT_ID");
            AddForeignKey("dbo.EF_CONTENT_DOUBLE_FIELD", "CONTENT_ID", "dbo.EF_CONTENT", "CONTENT_ID");
            AddForeignKey("dbo.EF_CONTENT_DESCRIPTION_FIELD", "CONTENT_ID", "dbo.EF_CONTENT", "CONTENT_ID");
        }
    }
}
