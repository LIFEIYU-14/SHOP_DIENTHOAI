namespace SHOP_DIENTHOAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB31 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PHAN_QUYEN", "Ten_Quyen", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PHAN_QUYEN", "Ten_Quyen", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
