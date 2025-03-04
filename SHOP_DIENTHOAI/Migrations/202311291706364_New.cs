namespace SHOP_DIENTHOAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SAN_PHAM", "TEN_SP", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SAN_PHAM", "TEN_SP", c => c.String(maxLength: 50));
        }
    }
}
