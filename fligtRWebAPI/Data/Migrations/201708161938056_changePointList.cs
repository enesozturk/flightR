namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changePointList : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Points", "PointListId", "dbo.PointLists");
            DropForeignKey("dbo.Users", "PointListId", "dbo.PointLists");
            DropIndex("dbo.Points", new[] { "PointListId" });
            DropIndex("dbo.Users", new[] { "PointListId" });
            AddColumn("dbo.Points", "Record_Id", c => c.Int());
            AddColumn("dbo.Records", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Records", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Points", "Record_Id");
            CreateIndex("dbo.Records", "UserId");
            AddForeignKey("dbo.Points", "Record_Id", "dbo.Records", "Id");
            AddForeignKey("dbo.Records", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            DropColumn("dbo.Users", "PointListId");
            DropColumn("dbo.Records", "Latitude");
            DropColumn("dbo.Records", "Longitude");
            DropColumn("dbo.Records", "Altitude");
            DropTable("dbo.PointLists");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PointLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Records", "Altitude", c => c.Double(nullable: false));
            AddColumn("dbo.Records", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.Records", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Users", "PointListId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Records", "UserId", "dbo.Users");
            DropForeignKey("dbo.Points", "Record_Id", "dbo.Records");
            DropIndex("dbo.Records", new[] { "UserId" });
            DropIndex("dbo.Points", new[] { "Record_Id" });
            DropColumn("dbo.Records", "UserId");
            DropColumn("dbo.Records", "CreatedDate");
            DropColumn("dbo.Points", "Record_Id");
            CreateIndex("dbo.Users", "PointListId");
            CreateIndex("dbo.Points", "PointListId");
            AddForeignKey("dbo.Users", "PointListId", "dbo.PointLists", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Points", "PointListId", "dbo.PointLists", "Id", cascadeDelete: true);
        }
    }
}
