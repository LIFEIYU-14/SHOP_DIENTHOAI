using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using SHOP_DIENTHOAI.Models;

namespace SHOP_DIENTHOAI.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        public ActionResult Index(string search = "", string sortBy = "Sắp xếp theo", int page = 1)
        {
            ModelDienThoai dt = new ModelDienThoai();
            var products = dt.SAN_PHAM.Where(row => row.TEN_SP.Contains(search));
            if (search == null)
            {
                return HttpNotFound();
            }
            switch (sortBy)
            {
                case "Giá Bán tăng dần":
                    products = products.OrderBy(row => row.GIA);
                    break;
                case "Giá Bán giảm dần":
                    products = products.OrderByDescending(row => row.GIA);
                    break;
                case "Tên Sản Phẩm tăng dần":
                    products = products.OrderBy(row => row.TEN_SP);
                    break;
                case "Tên Sản Phẩm giảm dần":
                    products = products.OrderByDescending(row => row.TEN_SP);
                    break;
                default:
                    products = products.OrderBy(row => row.GIA);
                    break;
            }

            ViewBag.Search = search;
            ViewBag.SortBy = sortBy;
            int pageSize = 8; // Số sản phẩm trên mỗi trang
            int pageNumber = (page < 1) ? 1 : page; // Trang hiện tại

            // Sử dụng Skip và Take để phân trang sản phẩm
            var pagedProducts = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Tính tổng số trang
            int totalProducts = products.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            // Pass thông tin phân trang qua ViewBag
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;

            return View(pagedProducts);
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
        [HttpPost]
        [ValidateAntiForgeryToken] // Bảo vệ chống tấn công CSRF
        public ActionResult ThemBinhLuan(int productId, string commentContent)
        {
            ModelDienThoai dt = new ModelDienThoai();

            if (Session["use"] == null)
            {
                TempData["ErrorMessage"] = "Bạn cần đăng nhập để bình luận!";
                return RedirectToAction("Dangnhap", "User");
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
        private ModelDienThoai dt = new ModelDienThoai();
        public List<SAN_PHAM> RandomSanPham(int soLuong)
        {
            return dt.SAN_PHAM.OrderBy(r => Guid.NewGuid()).Take(soLuong).ToList();
        }

        public ActionResult SanPhamTuongTu()
        {
            ModelDienThoai dt = new ModelDienThoai();
            var rdSP = RandomSanPham(3);
            return PartialView("SanPhamTuongTu", rdSP);
        }
    }

}