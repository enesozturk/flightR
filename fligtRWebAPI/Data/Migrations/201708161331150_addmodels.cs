namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmodels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lists", "RecordId", "dbo.Records");
            DropForeignKey("dbo.Lists", "RecordListId", "dbo.RecordLists");
            DropIndex("dbo.Lists", new[] { "RecordListId" });
            DropIndex("dbo.Lists", new[] { "RecordId" });
            DropTable("dbo.Lists");
            DropTable("dbo.RecordLists");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RecordLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Lists", "RecordId");
            CreateIndex("dbo.Lists", "RecordListId");
            AddForeignKey("dbo.Lists", "RecordListId", "dbo.RecordLists", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Lists", "RecordId", "dbo.Records", "Id", cascadeDelete: true);
        }
    }
}
