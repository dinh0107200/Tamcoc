namespace Tamcoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailOrderToConfig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConfigSites", "EmailOrder", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConfigSites", "EmailOrder");
        }
    }
}
