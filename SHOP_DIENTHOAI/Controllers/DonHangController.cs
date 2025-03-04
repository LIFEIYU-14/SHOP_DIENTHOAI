using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SHOP_DIENTHOAI.Models;

namespace SHOP_DIENTHOAI.Controllers
{
    public class DonHangController : Controller
    {
        ModelDienThoai dt = new ModelDienThoai();

        // Hiển thị danh sách đơn hàng
        public ActionResult Index()
        {
            if (Session["use"] == null || Session["use"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "User");
            }

            NGUOI_DUNG kh = (NGUOI_DUNG)Session["use"];
            int maND = kh.MA_ND;
            var donhangs = dt.DON_HANG.Include(d => d.NGUOI_DUNG).Where(d => d.MA_ND == maND);
            return View(donhangs.ToList());
        }

        // Xem chi tiết đơn hàng
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DON_HANG donhang = dt.DON_HANG.Find(id);
            var chitiet = dt.CHI_TIET_DON_HANG.Include(d => d.SAN_PHAM).Where(d => d.MA_DON == id).ToList();

            if (donhang == null)
            {
                return HttpNotFound();
            }

            return View(chitiet);
        }

        // Xử lý hủy đơn hàng
        [HttpPost]
        public JsonResult CancelOrder(int id)
        {
            var order = dt.DON_HANG.Find(id);
            if (order == null)
            {
                return Json(new { success = false, message = "Đơn hàng không tồn tại!" });
            }

            // Kiểm tra điều kiện hủy đơn hàng
            if (order.TINH_TRANG != 0 || order.THANHTOAN != 1)
            {
                return Json(new { success = false, message = "Không thể hủy đơn hàng này!" });
            }

            // Xóa đơn hàng khỏi cơ sở dữ liệu
            dt.DON_HANG.Remove(order);
            dt.SaveChanges();

            return Json(new { success = true, message = "Đơn hàng đã được hủy thành công!" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dt.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
