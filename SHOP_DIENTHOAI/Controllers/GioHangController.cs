using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHOP_DIENTHOAI.Models;

namespace SHOP_DIENTHOAI.Controllers
{
    public class GioHangController : Controller
    {
        ModelDienThoai dt = new ModelDienThoai();

        // Lấy giỏ hàng từ Session
        public List<GIO_HANG> LayGioHang()
        {
            List<GIO_HANG> lstGioHang = Session["GioHang"] as List<GIO_HANG>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GIO_HANG>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        public ActionResult ThemGioHang(int iMasp, string strURL)
        {
            SAN_PHAM sp = dt.SAN_PHAM.FirstOrDefault(row => row.MA_SP == iMasp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Kiểm tra nếu sản phẩm hết hàng
            if (sp.SOLUONG <= 0)
            {
                return Content("<script>alert('Sản phẩm này đã hết hàng!'); window.location='" + strURL + "';</script>");
            }

            List<GIO_HANG> lstGioHang = LayGioHang();
            GIO_HANG sanpham = lstGioHang.Find(n => n.iMasp == iMasp);

            if (sanpham == null)
            {
                sanpham = new GIO_HANG(iMasp);
                if (sanpham.iSoLuong > sp.SOLUONG)
                {
                    return Content("<script>alert('Không đủ hàng trong kho!'); window.location='" + strURL + "';</script>");
                }
                lstGioHang.Add(sanpham);
            }
            else
            {
                if (sanpham.iSoLuong + 1 > sp.SOLUONG)
                {
                    return Content("<script>alert('Không đủ hàng trong kho!'); window.location='" + strURL + "';</script>");
                }
                sanpham.iSoLuong++;
            }
            return RedirectToAction("GioHang");
        }


        public ActionResult UpdateQuantity(int sl = 0, int MaSP = 0)
        {
            if (sl <= 0)
            {
                return Content("<script>alert('Số lượng sản phẩm không hợp lệ!'); window.location='/GioHang/GioHang';</script>");
            }

            SAN_PHAM sp = dt.SAN_PHAM.SingleOrDefault(p => p.MA_SP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            if (sl > sp.SOLUONG)
            {
                return Content("<script>alert('Số lượng sản phẩm trong kho không đủ!'); window.location='/GioHang/GioHang';</script>");
            }

            List<GIO_HANG> lstGioHang = LayGioHang();
            GIO_HANG cartItem = lstGioHang.SingleOrDefault(n => n.iMasp == MaSP);
            if (cartItem != null)
            {
                cartItem.iSoLuong = sl;
            }

            return RedirectToAction("GioHang");
        }


        // Xóa giỏ hàng
        public ActionResult XoaGioHang(int iMaSP)
        {
            SAN_PHAM sp = dt.SAN_PHAM.SingleOrDefault(n => n.MA_SP == iMaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GIO_HANG> lstGioHang = LayGioHang();
            GIO_HANG sanpham = lstGioHang.SingleOrDefault(n => n.iMasp == iMaSP);

            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMasp == iMaSP);
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GIO_HANG> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }

        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GIO_HANG> lstGioHang = Session["GioHang"] as List<GIO_HANG>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }

        // Tính tổng thành tiền
        private double TongTien()
        {
            double dTongTien = 0;
            List<GIO_HANG> lstGioHang = Session["GioHang"] as List<GIO_HANG>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }

        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GIO_HANG> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }

        [HttpPost]
        public ActionResult DatHang(FormCollection donhangForm)
        {
            if (Session["use"] == null || Session["use"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "User");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<GIO_HANG> gh = LayGioHang();

            foreach (var item in gh)
            {
                SAN_PHAM sp = dt.SAN_PHAM.SingleOrDefault(p => p.MA_SP == item.iMasp);
                if (sp == null || sp.SOLUONG < item.iSoLuong)
                {
                    return Content($"<script>alert('Sản phẩm {item.sTensp} không đủ hàng trong kho!'); window.location='/GioHang';</script>");
                }
            }

            // Lấy thông tin đơn hàng từ form
            string diachinhanhang = donhangForm["DIACHI_NHANHANG"]?.Trim();
            string sdtNhanHang = donhangForm["SDT_NHANHANG"]?.Trim();
            int ptthanhtoan = Int32.Parse(donhangForm["MaTT"]);
            if (string.IsNullOrEmpty(diachinhanhang))
            {
                return Content("<script>alert('Vui lòng nhập địa chỉ nhận hàng!'); window.location='/GioHang/ThanhToanDonHang';</script>");
            }

            if (string.IsNullOrEmpty(sdtNhanHang))
            {
                return Content("<script>alert('Vui lòng nhập số điện thoại nhận hàng!'); window.location='/GioHang/ThanhToanDonHang';</script>");
            }
            // Tạo đơn hàng mới
            DON_HANG ddh = new DON_HANG
            {
                MA_ND = ((NGUOI_DUNG)Session["use"]).MA_ND,
                NGAY_DAT = DateTime.Now,
                THANHTOAN = ptthanhtoan,
                SDT_NHANHANG = sdtNhanHang,
                DIACHI_NHANHANG = diachinhanhang,
                TONG_TIEN = gh.Sum(item => item.iSoLuong * (int)item.dDonGia)
            };

            // Nếu thanh toán chuyển khoản (MaTT == 2) thì đơn hàng tự động xác nhận và giảm số lượng sản phẩm ngay
            if (ptthanhtoan == 2)
            {
                ddh.TINH_TRANG = 1; // Xác nhận đơn hàng tự động
            }
            else
            {
                ddh.TINH_TRANG = 0; // Chờ admin xác nhận
            }

            dt.DON_HANG.Add(ddh);
            dt.SaveChanges();

            // Thêm chi tiết đơn hàng
            foreach (var item in gh)
            {
                CHI_TIET_DON_HANG ctDH = new CHI_TIET_DON_HANG
                {
                    MA_DON = ddh.MA_DON,
                    MA_SP = item.iMasp,
                    SO_LUONG = item.iSoLuong,
                    DON_GIA = (int)item.dDonGia,
                    THANH_TIEN = item.iSoLuong * (int)item.dDonGia,
                    PHUONGTHUC_THANHTOAN = ptthanhtoan
                };

                dt.CHI_TIET_DON_HANG.Add(ctDH);
            }

            dt.SaveChanges();

            // Nếu đơn hàng được tự động xác nhận (thanh toán chuyển khoản) thì giảm số lượng sản phẩm ngay
            if (ptthanhtoan == 2)
            {
                foreach (var item in gh)
                {
                    SAN_PHAM sp = dt.SAN_PHAM.SingleOrDefault(p => p.MA_SP == item.iMasp);
                    if (sp != null)
                    {
                        sp.SOLUONG -= item.iSoLuong;
                    }
                }
                dt.SaveChanges();
            }

            Session["GioHang"] = null; // Xóa giỏ hàng sau khi đặt hàng thành công

            return RedirectToAction("Index", "Donhang");
        }

        public ActionResult ThanhToanDonHang()
        {
            ViewBag.MaTT = new SelectList(new[]
            {
                new { MaTT = 1, TenPT="Thanh toán tiền mặt" },
                new { MaTT = 2, TenPT="Thanh toán chuyển khoản" },
            }, "MaTT", "TenPT", 1);

            if (Session["use"] == null || Session["use"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            DON_HANG ddh = new DON_HANG();
            NGUOI_DUNG kh = (NGUOI_DUNG)Session["use"];
            List<GIO_HANG> gh = LayGioHang();
            decimal tongtien = 0;
            foreach (var item in gh)
            {
                decimal thanhtien = item.iSoLuong * (decimal)item.dDonGia;
                tongtien += thanhtien;
            }

            ddh.MA_ND = kh.MA_ND;
            ddh.NGAY_DAT = DateTime.Now;
            ViewBag.TenNguoiDatHang = kh.TEN_ND;
            CHI_TIET_DON_HANG ctDH = new CHI_TIET_DON_HANG();
            ViewBag.tongtien = tongtien;
            ViewBag.GioHang = gh;
            return View(ddh);
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
