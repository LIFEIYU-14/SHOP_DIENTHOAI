using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHOP_DIENTHOAI.Models;

namespace SHOP_DIENTHOAI.Controllers
{
    public class HangSanXuatController : Controller
    {

        public ActionResult BrandList()
        {
            ModelDienThoai dt = new ModelDienThoai();
            List<HANG_SAN_XUAT> brs = dt.HANG_SAN_XUAT.ToList();
            return PartialView("BrandList", brs);
        }
        public ActionResult BrandProduct(int id, int page = 1, string sortBy = "")
        {
            ModelDienThoai dt = new ModelDienThoai();
            List<HANG_SAN_XUAT> brs = dt.HANG_SAN_XUAT.ToList();
            List<SAN_PHAM> sp = dt.SAN_PHAM.Where(row => row.MA_HSX == id).ToList();
            switch (sortBy)
            {
                case "Giá Bán tăng dần":
                    sp = sp.OrderBy(product => product.GIA).ToList();
                    break;
                case "Giá Bán giảm dần":
                    sp = sp.OrderByDescending(product => product.GIA).ToList();
                    break;
                case "Tên Sản Phẩm tăng dần":
                    sp = sp.OrderBy(row => row.TEN_SP).ToList();
                    break;
                case "Tên Sản Phẩm giảm dần":
                    sp = sp.OrderByDescending(row => row.TEN_SP).ToList();
                    break;
                default:
                    sp = sp.OrderBy(product => product.TEN_SP).ToList();
                    break;
            }
            //tìm sản phẩm trong các Hãng
            foreach (SAN_PHAM product in sp)
            {
                if (product.MA_SP == id)
                {
                    ViewBag.TenSanPham = product.HANG_SAN_XUAT.TEN_HSX;
                    break;
                }
            }
            //paging
            int NoOfRecordPage = 4;
            int NoOfPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(sp.Count) / Convert.ToDouble(NoOfRecordPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPage;
            ViewBag.Page = page;
            ViewBag.NoOfPage = NoOfPage;
            sp = sp.Skip(NoOfRecordToSkip).Take(NoOfRecordPage).ToList();
      
            return View(sp);
        }
    }
}