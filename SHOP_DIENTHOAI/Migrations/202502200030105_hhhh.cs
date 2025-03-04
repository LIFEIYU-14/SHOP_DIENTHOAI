namespace SHOP_DIENTHOAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hhhh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ADMIN",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        TEN_ADMIN = c.String(nullable: false, maxLength: 50),
                        MATKHAU = c.String(nullable: false, maxLength: 60),
                        EMAIL = c.String(nullable: false, maxLength: 50),
                        DIEN_THOAI = c.String(nullable: false, maxLength: 10),
                        DIA_CHI = c.String(nullable: false),
                        ID_Quyen = c.Int(),
                    })
                .PrimaryKey(t => t.AdminId)
                .ForeignKey("dbo.PHAN_QUYEN", t => t.ID_Quyen)
                .Index(t => t.ID_Quyen);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ADMIN", "ID_Quyen", "dbo.PHAN_QUYEN");
            DropIndex("dbo.ADMIN", new[] { "ID_Quyen" });
            DropTable("dbo.ADMIN");
        }
    }
}
