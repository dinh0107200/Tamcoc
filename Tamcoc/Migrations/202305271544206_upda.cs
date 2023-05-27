namespace Tamcoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upda : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConfigSiteEns", "AboutUrl", c => c.String(maxLength: 500));
            AddColumn("dbo.ConfigSiteFrs", "AboutUrl", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConfigSiteFrs", "AboutUrl");
            DropColumn("dbo.ConfigSiteEns", "AboutUrl");
        }
    }
}
