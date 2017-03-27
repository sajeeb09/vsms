namespace VSMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asllr : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.customers", "invoiceDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.customers", "invoiceDate", c => c.DateTime(nullable: false));
        }
    }
}
