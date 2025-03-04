using System.Data.Entity.Migrations;
using System.Linq;
using SHOP_DIENTHOAI.Models;

namespace SHOP_DIENTHOAI.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ModelDienThoai>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SHOP_DIENTHOAI.Models.ModelDienThoai";
        }

        protected override void Seed(ModelDienThoai context)
        {
            // Seed phân quyền nếu chưa có
            if (!context.PHAN_QUYEN.Any())
            {
                context.PHAN_QUYEN.AddOrUpdate(
                    new PHAN_QUYEN { ID_Quyen = 1, Ten_Quyen = "User" },
                    new PHAN_QUYEN { ID_Quyen = 2, Ten_Quyen = "Admin" }
                );
                context.SaveChanges();
            }

            // Seed tài khoản Admin nếu chưa tồn tại
            if (!context.ADMIN.Any(a => a.EMAIL == "Admin@gmail.com"))
            {
                context.ADMIN.AddOrUpdate(new ADMIN
                {
                    TEN_ADMIN = "Admin",
                    EMAIL = "Admin@gmail.com",
                    DIEN_THOAI = "0123456789",
                    MATKHAU = PasswordHelper.HashPassword("Admin@123"), // Hash mật khẩu
                    DIA_CHI = "140 Đường Lê Trọng Tấn,Tân Phú,TP.HCM",
                    ID_Quyen = 2 // Gán quyền Admin
                });
                context.SaveChanges();
            }
        }
    }
}
