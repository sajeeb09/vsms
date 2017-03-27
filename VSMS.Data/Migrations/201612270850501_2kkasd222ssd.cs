namespace VSMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2kkasd222ssd : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.brands", new[] { "vehiclebrand" });
            AlterColumn("dbo.brands", "vehiclebrand", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.brands", "vehiclebrand", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.brands", new[] { "vehiclebrand" });
            AlterColumn("dbo.brands", "vehiclebrand", c => c.String(maxLength: 100));
            CreateIndex("dbo.brands", "vehiclebrand", unique: true);
        }
    }
}
