namespace SHOP_DIENTHOAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CHI_TIET_DON_HANG",
                c => new
                    {
                        MA_DON = c.Int(nullable: false),
                        MA_SP = c.Int(nullable: false),
                        SO_LUONG = c.Int(),
                        DON_GIA = c.Int(),
                        THANH_TIEN = c.Int(),
                        PHUONGTHUC_THANHTOAN = c.Int(),
                    })
                .PrimaryKey(t => new { t.MA_DON, t.MA_SP })
                .ForeignKey("dbo.DON_HANG", t => t.MA_DON, cascadeDelete: true)
                .ForeignKey("dbo.SAN_PHAM", t => t.MA_SP, cascadeDelete: true)
                .Index(t => t.MA_DON)
                .Index(t => t.MA_SP);
            
            CreateTable(
                "dbo.DON_HANG",
                c => new
                    {
                        MA_DON = c.Int(nullable: false, identity: true),
                        NGAY_DAT = c.DateTime(),
                        TINH_TRANG = c.Int(),
                        MA_ND = c.Int(),
                        THANHTOAN = c.Int(),
                        DIACHI_NHANHANG = c.String(),
                        TONG_TIEN = c.Int(),
                    })
                .PrimaryKey(t => t.MA_DON)
                .ForeignKey("dbo.NGUOI_DUNG", t => t.MA_ND)
                .Index(t => t.MA_ND);
            
            CreateTable(
                "dbo.NGUOI_DUNG",
                c => new
                    {
                        MA_ND = c.Int(nullable: false, identity: true),
                        TEN_ND = c.String(nullable: false, maxLength: 50),
                        MATKHAU = c.String(nullable: false, maxLength: 50),
                        EMAIL = c.String(nullable: false, maxLength: 50),
                        DIEN_THOAI = c.String(nullable: false, maxLength: 10),
                        ID_Quyen = c.Int(),
                        DIA_CHI = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MA_ND)
                .ForeignKey("dbo.PHAN_QUYEN", t => t.ID_Quyen)
                .Index(t => t.ID_Quyen);
            
            CreateTable(
                "dbo.PHAN_QUYEN",
                c => new
                    {
                        ID_Quyen = c.Int(nullable: false, identity: true),
                        Ten_Quyen = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.ID_Quyen);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CHI_TIET_DON_HANG", "MA_SP", "dbo.SAN_PHAM");
            DropForeignKey("dbo.NGUOI_DUNG", "ID_Quyen", "dbo.PHAN_QUYEN");
            DropForeignKey("dbo.DON_HANG", "MA_ND", "dbo.NGUOI_DUNG");
            DropForeignKey("dbo.CHI_TIET_DON_HANG", "MA_DON", "dbo.DON_HANG");
            DropIndex("dbo.NGUOI_DUNG", new[] { "ID_Quyen" });
            DropIndex("dbo.DON_HANG", new[] { "MA_ND" });
            DropIndex("dbo.CHI_TIET_DON_HANG", new[] { "MA_SP" });
            DropIndex("dbo.CHI_TIET_DON_HANG", new[] { "MA_DON" });
            DropTable("dbo.PHAN_QUYEN");
            DropTable("dbo.NGUOI_DUNG");
            DropTable("dbo.DON_HANG");
            DropTable("dbo.CHI_TIET_DON_HANG");
        }
    }
}
