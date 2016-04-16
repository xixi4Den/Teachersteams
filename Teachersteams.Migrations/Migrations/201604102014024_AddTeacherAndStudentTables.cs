namespace Teachersteams.Migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeacherAndStudentTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Uid = c.String(nullable: false),
                        GroupId = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Uid = c.String(nullable: false),
                        GroupId = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Student");
            DropTable("dbo.Teacher");
        }
    }
}
