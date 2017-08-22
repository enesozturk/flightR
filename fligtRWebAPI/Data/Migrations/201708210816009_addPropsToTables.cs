namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPropsToTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Points", "Speed", c => c.Double(nullable: false));
            AddColumn("dbo.Records", "Distance", c => c.Single(nullable: false));
            AddColumn("dbo.Records", "Duration", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Records", "Duration");
            DropColumn("dbo.Records", "Distance");
            DropColumn("dbo.Points", "Speed");
        }
    }
}
