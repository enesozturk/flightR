namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Points", "PointListId", "dbo.PointLists");
            DropIndex("dbo.Points", new[] { "PointListId" });
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
            
            CreateIndex("dbo.Points", "PointListId");
            AddForeignKey("dbo.Points", "PointListId", "dbo.PointLists", "Id", cascadeDelete: true);
        }
    }
}
