namespace Day2Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyThirdMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Grades", "CourseTitle", c => c.String(nullable: false));
            AlterColumn("dbo.Grades", "Passed", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Grades", "Passed", c => c.String());
            AlterColumn("dbo.Grades", "CourseTitle", c => c.String());
        }
    }
}
