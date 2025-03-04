using System;
using System.Data.Entity.Migrations;

namespace SHOP_DIENTHOAI.Migrations
{
    public partial class Remove_DON_HANG : DbMigration
    {
        public override void Up()
        {
            Sql("DROP TABLE DON_HANG"); // Xóa bảng DON_HANG
        }

        public override void Down()
        {
            // Nếu cần rollback, hãy thêm lại bảng DON_HANG
            CreateTable(
                "dbo.DON_HANG",
                c => new
                {
                    MA_DON = c.Int(nullable: false, identity: true),
                    TEN_DON = c.String(),
                })
                .PrimaryKey(t => t.MA_DON);
        }
    }
}
