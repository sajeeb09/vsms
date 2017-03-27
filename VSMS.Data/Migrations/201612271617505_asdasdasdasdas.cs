namespace VSMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdasdasdasdas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.vehicles", "bdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.vehicles", "bdate");
        }
    }
}
