namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPoint : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Points",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Altitude = c.Double(nullable: false),
                        PointListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PointLists", t => t.PointListId, cascadeDelete: true)
                .Index(t => t.PointListId);
            
            CreateTable(
                "dbo.PointLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Points", "PointListId", "dbo.PointLists");
            DropIndex("dbo.Points", new[] { "PointListId" });
            DropTable("dbo.PointLists");
            DropTable("dbo.Points");
        }
    }
}
