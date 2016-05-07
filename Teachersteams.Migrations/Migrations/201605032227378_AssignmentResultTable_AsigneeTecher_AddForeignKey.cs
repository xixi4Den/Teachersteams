namespace Teachersteams.Migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignmentResultTable_AsigneeTecher_AddForeignKey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AssignmentResult", "AssigneeTeacherId");
            AddForeignKey("dbo.AssignmentResult", "AssigneeTeacherId", "dbo.Teacher", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignmentResult", "AssigneeTeacherId", "dbo.Teacher");
            DropIndex("dbo.AssignmentResult", new[] { "AssigneeTeacherId" });
        }
    }
}
