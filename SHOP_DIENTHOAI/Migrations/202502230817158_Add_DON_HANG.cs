using System;
using System.Data.Entity.Migrations;

namespace SHOP_DIENTHOAI.Migrations
{
    public partial class Add_DON_HANG : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DON_HANG",
                c => new
                {
                    MA_DON = c.Int(nullable: false, identity: true),
                    NGAY_DAT = c.DateTime(),
                    TINH_TRANG = c.Int(),
                    MA_ND = c.Int(),
                    THANHTOAN = c.Int(),
                    DIACHI_NHANHANG = c.String(nullable: false, maxLength: 500),
                    SDT_NHANHANG = c.String(nullable: false, maxLength: 10),
                    TONG_TIEN = c.Int(),
                })
                .PrimaryKey(t => t.MA_DON)
                .ForeignKey("dbo.NGUOI_DUNG", t => t.MA_ND)
                .Index(t => t.MA_ND);
        }

        public override void Down()
        {
            DropForeignKey("dbo.DON_HANG", "MA_ND", "dbo.NGUOI_DUNG");
            DropIndex("dbo.DON_HANG", new[] { "MA_ND" });
            DropTable("dbo.DON_HANG");
        }
    }
}
