using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SHOP_DIENTHOAI.Models;

namespace SHOP_DIENTHOAI.Areas.Admin.Controllers
{
    public class HangSanXuatApiController : ApiController
    {
        public List<HANG_SAN_XUAT> Get()
        {
            ModelDienThoai dt = new ModelDienThoai();
            List<HANG_SAN_XUAT> hsx = dt.HANG_SAN_XUAT.ToList();
            return hsx;
        }
    
        public HANG_SAN_XUAT GetmaHSX(int ID)
        {
            ModelDienThoai dt = new ModelDienThoai();
            HANG_SAN_XUAT hsx = dt.HANG_SAN_XUAT.Where(row => row.MA_HSX == ID).FirstOrDefault();
            return hsx;
        }

        public void PostLoaiSanPham(HANG_SAN_XUAT newHSX)
        {
            ModelDienThoai dt = new ModelDienThoai();

            dt.HANG_SAN_XUAT.Add(newHSX);
            dt.SaveChanges();
        }

        public void PutLoaiSanPham(HANG_SAN_XUAT hsx)
        {
            ModelDienThoai dt = new ModelDienThoai();
            HANG_SAN_XUAT oldhsx = dt.HANG_SAN_XUAT.Where(r => r.MA_HSX == hsx.MA_HSX).FirstOrDefault();
            oldhsx.TEN_HSX = hsx.TEN_HSX;
            dt.SaveChanges();
            dt.SaveChanges();
        }

        public void DeleteHSX(int id)
        {
            ModelDienThoai dt = new ModelDienThoai();
            HANG_SAN_XUAT oldhsx = dt.HANG_SAN_XUAT.Where(r => r.MA_HSX == id).FirstOrDefault();
            dt.HANG_SAN_XUAT.Remove(oldhsx);
            dt.SaveChanges();
        }
    }

}
