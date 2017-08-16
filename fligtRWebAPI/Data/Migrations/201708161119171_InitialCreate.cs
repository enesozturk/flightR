namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Records",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Altitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecordListId = c.Int(nullable: false),
                        RecordId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Records", t => t.RecordId, cascadeDelete: true)
                .ForeignKey("dbo.RecordLists", t => t.RecordListId, cascadeDelete: true)
                .Index(t => t.RecordListId)
                .Index(t => t.RecordId);
            
            CreateTable(
                "dbo.RecordLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lists", "RecordListId", "dbo.RecordLists");
            DropForeignKey("dbo.Lists", "RecordId", "dbo.Records");
            DropIndex("dbo.Lists", new[] { "RecordId" });
            DropIndex("dbo.Lists", new[] { "RecordListId" });
            DropTable("dbo.RecordLists");
            DropTable("dbo.Lists");
            DropTable("dbo.Records");
        }
    }
}
