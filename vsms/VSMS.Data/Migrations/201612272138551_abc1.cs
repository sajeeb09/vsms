namespace VSMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abc1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.vehicles", "sprice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.vehicles", "sprice");
        }
    }
}
