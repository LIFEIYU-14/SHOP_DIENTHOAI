namespace SHOP_DIENTHOAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HANG_SAN_XUAT",
                c => new
                    {
                        MA_HSX = c.Int(nullable: false, identity: true),
                        TEN_HSX = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.MA_HSX);
            
            CreateTable(
                "dbo.SAN_PHAM",
                c => new
                    {
                        MA_SP = c.Int(nullable: false, identity: true),
                        TEN_SP = c.String(maxLength: 50),
                        MA_HSX = c.Int(),
                        GIA = c.Int(),
                        GHICHU_SP = c.String(),
                        HinhAnh = c.String(),
                        Hinh1 = c.String(),
                        Hinh2 = c.String(),
                        Hinh3 = c.String(),
                        Hinh4 = c.String(),
                    })
                .PrimaryKey(t => t.MA_SP)
                .ForeignKey("dbo.HANG_SAN_XUAT", t => t.MA_HSX)
                .Index(t => t.MA_HSX);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SAN_PHAM", "MA_HSX", "dbo.HANG_SAN_XUAT");
            DropIndex("dbo.SAN_PHAM", new[] { "MA_HSX" });
            DropTable("dbo.SAN_PHAM");
            DropTable("dbo.HANG_SAN_XUAT");
        }
    }
}
