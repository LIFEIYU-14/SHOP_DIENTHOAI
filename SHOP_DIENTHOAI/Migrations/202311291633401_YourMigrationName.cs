namespace SHOP_DIENTHOAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YourMigrationName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CHI_TIET_DON_HANG", "MA_DON", "dbo.DON_HANG");
            DropForeignKey("dbo.CHI_TIET_DON_HANG", "MA_SP", "dbo.SAN_PHAM");
            AlterColumn("dbo.NGUOI_DUNG", "MATKHAU", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.NGUOI_DUNG", "DIEN_THOAI", c => c.String(nullable: false, maxLength: 10));
            AddForeignKey("dbo.CHI_TIET_DON_HANG", "MA_DON", "dbo.DON_HANG", "MA_DON", cascadeDelete: true);
            AddForeignKey("dbo.CHI_TIET_DON_HANG", "MA_SP", "dbo.SAN_PHAM", "MA_SP", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CHI_TIET_DON_HANG", "MA_SP", "dbo.SAN_PHAM");
            DropForeignKey("dbo.CHI_TIET_DON_HANG", "MA_DON", "dbo.DON_HANG");
            AlterColumn("dbo.NGUOI_DUNG", "DIEN_THOAI", c => c.String(nullable: false, maxLength: 10, fixedLength: true));
            AlterColumn("dbo.NGUOI_DUNG", "MATKHAU", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddForeignKey("dbo.CHI_TIET_DON_HANG", "MA_SP", "dbo.SAN_PHAM", "MA_SP");
            AddForeignKey("dbo.CHI_TIET_DON_HANG", "MA_DON", "dbo.DON_HANG", "MA_DON");
        }
    }
}
