namespace Teachersteams.Migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentAndTeacherTablesAddForeignKeyToGroupTable : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Teacher", "GroupId");
            CreateIndex("dbo.Student", "GroupId");
            AddForeignKey("dbo.Teacher", "GroupId", "dbo.Group", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Student", "GroupId", "dbo.Group", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Student", "GroupId", "dbo.Group");
            DropForeignKey("dbo.Teacher", "GroupId", "dbo.Group");
            DropIndex("dbo.Student", new[] { "GroupId" });
            DropIndex("dbo.Teacher", new[] { "GroupId" });
        }
    }
}
