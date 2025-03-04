using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using SHOP_DIENTHOAI.Models;

namespace SHOP_DIENTHOAI.Areas.Admin.Controllers
{
    public class DonHangAdminController : Controller
    {
        private readonly ModelDienThoai _context = new ModelDienThoai();

        public ActionResult Index()
        {
            var donhangs = _context.DON_HANG.Include(d => d.NGUOI_DUNG).AsNoTracking().ToList();
            return View(donhangs);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var donhang = _context.DON_HANG.Find(id);
            if (donhang == null)
                return HttpNotFound();

            return View(donhang);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var donhang = _context.DON_HANG.Find(id);
            if (donhang == null)
                return HttpNotFound();

            return View(donhang);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var donHang = _context.DON_HANG.Include(d => d.CHI_TIET_DON_HANG).FirstOrDefault(d => d.MA_DON == id);
            if (donHang == null)
                return HttpNotFound();

            _context.DON_HANG.Remove(donHang);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult XacNhan(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var donHang = _context.DON_HANG.Include(d => d.CHI_TIET_DON_HANG).FirstOrDefault(d => d.MA_DON == id);
            if (donHang == null)
                return HttpNotFound();

            if (donHang.THANHTOAN == 2)
            {
                donHang.TINH_TRANG = 2;
            }
            else if (donHang.TINH_TRANG == 0)
            {
                bool hasError = false;
                foreach (var chiTiet in donHang.CHI_TIET_DON_HANG)
                {
                    var sanPham = _context.SAN_PHAM.Find(chiTiet.MA_SP);
                    if (sanPham == null || sanPham.SOLUONG < chiTiet.SO_LUONG)
                    {
                        TempData["ErrorMessage"] = $"Sản phẩm '{sanPham?.TEN_SP}' không đủ hàng!";
                        hasError = true;
                        break;
                    }
                    sanPham.SOLUONG -= chiTiet.SO_LUONG;
                    _context.Entry(sanPham).State = EntityState.Modified;
                }
                if (!hasError)
                {
                    donHang.TINH_TRANG = 1;
                }
            }
            else if (donHang.TINH_TRANG == 1)
            {
                donHang.TINH_TRANG = 2;
            }

            _context.Entry(donHang).State = EntityState.Modified;
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Đơn hàng đã được cập nhật.";
            return RedirectToAction("Index");
        }

      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
