namespace SHOP_DIENTHOAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SAN_PHAMCOMMENT", "SAN_PHAM_MA_SP", "dbo.SAN_PHAM");
            DropForeignKey("dbo.SAN_PHAMCOMMENT", "COMMENT_MA_CMT", "dbo.COMMENTs");
            DropForeignKey("dbo.COMMENTs", "NGUOI_DUNG_MA_ND", "dbo.NGUOI_DUNG");
            DropIndex("dbo.COMMENTs", new[] { "NGUOI_DUNG_MA_ND" });
            DropIndex("dbo.SAN_PHAMCOMMENT", new[] { "SAN_PHAM_MA_SP" });
            DropIndex("dbo.SAN_PHAMCOMMENT", new[] { "COMMENT_MA_CMT" });
            RenameColumn(table: "dbo.COMMENTs", name: "NGUOI_DUNG_MA_ND", newName: "MA_ND");
            AddColumn("dbo.COMMENTs", "NGAY_CMT", c => c.DateTime(nullable: false));
            AlterColumn("dbo.COMMENTs", "NOIDUNG_CMT", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.COMMENTs", "MA_ND", c => c.Int(nullable: false));
            CreateIndex("dbo.COMMENTs", "MA_ND");
            CreateIndex("dbo.COMMENTs", "MA_SP");
            AddForeignKey("dbo.COMMENTs", "MA_SP", "dbo.SAN_PHAM", "MA_SP", cascadeDelete: true);
            AddForeignKey("dbo.COMMENTs", "MA_ND", "dbo.NGUOI_DUNG", "MA_ND", cascadeDelete: true);
            DropTable("dbo.SAN_PHAMCOMMENT");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SAN_PHAMCOMMENT",
                c => new
                    {
                        SAN_PHAM_MA_SP = c.Int(nullable: false),
                        COMMENT_MA_CMT = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SAN_PHAM_MA_SP, t.COMMENT_MA_CMT });
            
            DropForeignKey("dbo.COMMENTs", "MA_ND", "dbo.NGUOI_DUNG");
            DropForeignKey("dbo.COMMENTs", "MA_SP", "dbo.SAN_PHAM");
            DropIndex("dbo.COMMENTs", new[] { "MA_SP" });
            DropIndex("dbo.COMMENTs", new[] { "MA_ND" });
            AlterColumn("dbo.COMMENTs", "MA_ND", c => c.Int());
            AlterColumn("dbo.COMMENTs", "NOIDUNG_CMT", c => c.String());
            DropColumn("dbo.COMMENTs", "NGAY_CMT");
            RenameColumn(table: "dbo.COMMENTs", name: "MA_ND", newName: "NGUOI_DUNG_MA_ND");
            CreateIndex("dbo.SAN_PHAMCOMMENT", "COMMENT_MA_CMT");
            CreateIndex("dbo.SAN_PHAMCOMMENT", "SAN_PHAM_MA_SP");
            CreateIndex("dbo.COMMENTs", "NGUOI_DUNG_MA_ND");
            AddForeignKey("dbo.COMMENTs", "NGUOI_DUNG_MA_ND", "dbo.NGUOI_DUNG", "MA_ND");
            AddForeignKey("dbo.SAN_PHAMCOMMENT", "COMMENT_MA_CMT", "dbo.COMMENTs", "MA_CMT", cascadeDelete: true);
            AddForeignKey("dbo.SAN_PHAMCOMMENT", "SAN_PHAM_MA_SP", "dbo.SAN_PHAM", "MA_SP", cascadeDelete: true);
        }
    }
}
