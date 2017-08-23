namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPointListDbSet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PointLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Points", "PointListId");
            AddForeignKey("dbo.Points", "PointListId", "dbo.PointLists", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Points", "PointListId", "dbo.PointLists");
            DropIndex("dbo.Points", new[] { "PointListId" });
            DropTable("dbo.PointLists");
        }
    }
}
