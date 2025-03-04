using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace SHOP_DIENTHOAI.Models
{
    public class ModelDienThoai :DbContext
    {
        public ModelDienThoai(): base("ModelDienThoai")
        {

        }

        public DbSet<HANG_SAN_XUAT> HANG_SAN_XUAT { get; set; }
        public DbSet<SAN_PHAM> SAN_PHAM { get; set; }
        public DbSet<NGUOI_DUNG> NGUOI_DUNG { get; set; }
        public DbSet<PHAN_QUYEN> PHAN_QUYEN { get; set; }
        public DbSet<CHI_TIET_DON_HANG> CHI_TIET_DON_HANG { get; set; }
        public DbSet<DON_HANG> DON_HANG { get; set; }
        public DbSet<COMMENT> COMMENT { get; set; }
        public DbSet<ADMIN> ADMIN { get; set; }

    }
}