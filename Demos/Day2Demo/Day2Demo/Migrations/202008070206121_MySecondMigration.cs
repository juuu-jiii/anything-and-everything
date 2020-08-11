namespace Day2Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MySecondMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Grades", "Passed", c => c.String());
            DropTable("dbo.Students");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Year = c.Int(nullable: false),
                        College = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Grades", "Passed", c => c.Boolean(nullable: false));
        }
    }
}
