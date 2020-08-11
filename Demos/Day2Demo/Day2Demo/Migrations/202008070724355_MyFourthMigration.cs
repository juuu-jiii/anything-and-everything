namespace Day2Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyFourthMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Grades", "Passed", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Grades", "Passed", c => c.String(nullable: false));
        }
    }
}
