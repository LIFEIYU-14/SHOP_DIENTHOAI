using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHOP_DIENTHOAI.Models;

namespace SHOP_DIENTHOAI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ModelDienThoai dt = new ModelDienThoai();
        public List<SAN_PHAM> RandomSanPham(int soLuong)
        {

            return dt.SAN_PHAM.OrderBy(r => Guid.NewGuid()).Take(soLuong).ToList();
        }

        public ActionResult SanPhamTuongTu()
        {
            ModelDienThoai dt = new ModelDienThoai();
            var rdSP = RandomSanPham(4);
            return PartialView("SanPhamTuongTu", rdSP);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // Bảo vệ chống tấn công CSRF
        public ActionResult ThemBinhLuan(int productId, string commentContent)
        {
            if (Session["use"] == null)
            {
                TempData["ErrorMessage"] = "Bạn cần đăng nhập để bình luận!";
                return RedirectToAction("DangNhap", "User");
            }

            if (string.IsNullOrWhiteSpace(commentContent))
            {
                TempData["ErrorMessage"] = "Bình luận không được để trống!";
                return RedirectToAction("ChiTiet", new { id = productId });
            }

            var kh = (NGUOI_DUNG)Session["use"];
            var comment = new COMMENT
            {
                NOIDUNG_CMT = commentContent,
                MA_SP = productId,
                MA_ND = kh.MA_ND,
                NGAY_CMT = DateTime.Now
            };

            dt.COMMENT.Add(comment);
            dt.SaveChanges();

            TempData["SuccessMessage"] = "Bình luận của bạn đã được đăng!";
            return RedirectToAction("ChiTiet", new { id = productId });
        }

        public ActionResult ChiTiet(int id)
        {
            ModelDienThoai dt = new ModelDienThoai();
            var sp = dt.SAN_PHAM
                    .Include("COMMENT.NGUOI_DUNG")
                    .FirstOrDefault(row => row.MA_SP == id);
            if (sp != null)
            {
                return View(sp);
            }
            else
            {
                return HttpNotFound();
            }
        }

    }
}