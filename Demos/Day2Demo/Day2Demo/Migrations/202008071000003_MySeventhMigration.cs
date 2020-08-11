namespace Day2Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MySeventhMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Grades", "ResultText");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Grades", "ResultText", c => c.String());
        }
    }
}
