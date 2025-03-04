namespace SHOP_DIENTHOAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpUp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NGUOI_DUNG", "MATKHAU", c => c.String(nullable: false, maxLength: 60));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NGUOI_DUNG", "MATKHAU", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
