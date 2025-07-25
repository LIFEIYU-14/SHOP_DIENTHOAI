namespace SHOP_DIENTHOAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DonHang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DON_HANG", "SDT_NHANHANG", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DON_HANG", "SDT_NHANHANG");
        }
    }
}
