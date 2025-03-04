namespace SHOP_DIENTHOAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.COMMENTs",
                c => new
                    {
                        MA_CMT = c.Int(nullable: false, identity: true),
                        NOIDUNG_CMT = c.String(),
                        MA_SP = c.Int(nullable: false),
                        NGUOI_DUNG_MA_ND = c.Int(),
                    })
                .PrimaryKey(t => t.MA_CMT)
                .ForeignKey("dbo.NGUOI_DUNG", t => t.NGUOI_DUNG_MA_ND)
                .Index(t => t.NGUOI_DUNG_MA_ND);
            
            CreateTable(
                "dbo.SAN_PHAMCOMMENT",
                c => new
                    {
                        SAN_PHAM_MA_SP = c.Int(nullable: false),
                        COMMENT_MA_CMT = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SAN_PHAM_MA_SP, t.COMMENT_MA_CMT })
                .ForeignKey("dbo.SAN_PHAM", t => t.SAN_PHAM_MA_SP, cascadeDelete: true)
                .ForeignKey("dbo.COMMENTs", t => t.COMMENT_MA_CMT, cascadeDelete: true)
                .Index(t => t.SAN_PHAM_MA_SP)
                .Index(t => t.COMMENT_MA_CMT);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SAN_PHAMCOMMENT", "COMMENT_MA_CMT", "dbo.COMMENTs");
            DropForeignKey("dbo.SAN_PHAMCOMMENT", "SAN_PHAM_MA_SP", "dbo.SAN_PHAM");
            DropForeignKey("dbo.COMMENTs", "NGUOI_DUNG_MA_ND", "dbo.NGUOI_DUNG");
            DropIndex("dbo.SAN_PHAMCOMMENT", new[] { "COMMENT_MA_CMT" });
            DropIndex("dbo.SAN_PHAMCOMMENT", new[] { "SAN_PHAM_MA_SP" });
            DropIndex("dbo.COMMENTs", new[] { "NGUOI_DUNG_MA_ND" });
            DropTable("dbo.SAN_PHAMCOMMENT");
            DropTable("dbo.COMMENTs");
        }
    }
}
