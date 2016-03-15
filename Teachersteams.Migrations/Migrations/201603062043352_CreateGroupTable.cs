using System.Data.Entity.Migrations;

namespace Teachersteams.Migrations.Migrations
{
    public partial class CreateGroupTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        OwnerId = c.String(nullable: false),
                        PictureLink = c.String(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Group");
        }
    }
}
