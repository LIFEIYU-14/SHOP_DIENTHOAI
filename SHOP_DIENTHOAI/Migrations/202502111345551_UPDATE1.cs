namespace SHOP_DIENTHOAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPDATE1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SAN_PHAM", "SOLUONG", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SAN_PHAM", "SOLUONG");
        }
    }
}
