namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Points", "Record_Id", "dbo.Records");
            DropIndex("dbo.Points", new[] { "Record_Id" });
            RenameColumn(table: "dbo.Points", name: "Record_Id", newName: "RecordId");
            AlterColumn("dbo.Points", "RecordId", c => c.Int(nullable: false));
            CreateIndex("dbo.Points", "RecordId");
            AddForeignKey("dbo.Points", "RecordId", "dbo.Records", "Id", cascadeDelete: true);
            DropColumn("dbo.Points", "PointListId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Points", "PointListId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Points", "RecordId", "dbo.Records");
            DropIndex("dbo.Points", new[] { "RecordId" });
            AlterColumn("dbo.Points", "RecordId", c => c.Int());
            RenameColumn(table: "dbo.Points", name: "RecordId", newName: "Record_Id");
            CreateIndex("dbo.Points", "Record_Id");
            AddForeignKey("dbo.Points", "Record_Id", "dbo.Records", "Id");
        }
    }
}
