namespace Day2Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MySixthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Grades", "ResultColor", c => c.String());
            AddColumn("dbo.Grades", "ResultText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Grades", "ResultText");
            DropColumn("dbo.Grades", "ResultColor");
        }
    }
}
