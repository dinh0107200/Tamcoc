namespace Tamcoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubHeaderImageToConfigSite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConfigSites", "SubHeaderImage", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConfigSites", "SubHeaderImage");
        }
    }
}
