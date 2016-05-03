namespace Teachersteams.Migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAssignmentResultTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignmentResult",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AssignmentId = c.Guid(nullable: false),
                        StudentId = c.Guid(nullable: false),
                        CompletionDate = c.DateTime(nullable: false),
                        File = c.String(nullable: false),
                        AssigneeTeacherId = c.Guid(),
                        Grade = c.Byte(),
                        CheckDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assignment", t => t.AssignmentId, cascadeDelete: false)
                .ForeignKey("dbo.Student", t => t.StudentId, cascadeDelete: false)
                .Index(t => t.AssignmentId)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignmentResult", "StudentId", "dbo.Student");
            DropForeignKey("dbo.AssignmentResult", "AssignmentId", "dbo.Assignment");
            DropIndex("dbo.AssignmentResult", new[] { "StudentId" });
            DropIndex("dbo.AssignmentResult", new[] { "AssignmentId" });
            DropTable("dbo.AssignmentResult");
        }
    }
}
