namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameSurname = c.String(),
                        UserName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        PointListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PointLists", t => t.PointListId, cascadeDelete: true)
                .Index(t => t.PointListId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "PointListId", "dbo.PointLists");
            DropIndex("dbo.Users", new[] { "PointListId" });
            DropTable("dbo.Users");
        }
    }
}
