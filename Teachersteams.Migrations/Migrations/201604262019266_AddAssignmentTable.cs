namespace Teachersteams.Migrations.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddAssignmentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignment",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        File = c.String(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        GroupId = c.Guid(nullable: false),
                        Creator = c.Guid(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Group", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignment", "GroupId", "dbo.Group");
            DropIndex("dbo.Assignment", new[] { "GroupId" });
            DropTable("dbo.Assignment");
        }
    }
}
