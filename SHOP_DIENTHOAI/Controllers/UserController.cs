using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHOP_DIENTHOAI.Models;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Data.Entity;
using System.Net.Mail;
using System.Text.RegularExpressions;
using BCrypt.Net;

namespace SHOP_DIENTHOAI.Controllers
{
    public class UserController : Controller
    {
        public int GetDefaultUserRoleID()
        {
            ModelDienThoai dt = new ModelDienThoai();
          
            var defaultUserRole = dt.PHAN_QUYEN.SingleOrDefault(r => r.Ten_Quyen == "User");
            if (defaultUserRole != null)
            {
                return defaultUserRole.ID_Quyen;
            }
            else
            {
                var newRole = new PHAN_QUYEN { Ten_Quyen = "User" };
                dt.PHAN_QUYEN.Add(newRole);
                dt.SaveChanges();
                return newRole.ID_Quyen;
            }
        }
        // Đăng ký người dùng mới
        public ActionResult DangKy()
        {
            return View();
        }

        // Xử lý đăng ký người dùng mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(NGUOI_DUNG nguoidung)
        {
            try
            {
                ModelDienThoai dt = new ModelDienThoai();

                if (!ModelState.IsValid)
                {
                    return View("DangKy");
                }

                nguoidung.ID_Quyen = GetDefaultUserRoleID();

                // Mã hóa mật khẩu
                nguoidung.MATKHAU = PasswordHelper.HashPassword(nguoidung.MATKHAU);

                dt.NGUOI_DUNG.Add(nguoidung);
                dt.SaveChanges();

                TempData["SuccessMessage"] = "Đăng ký thành công. Đăng nhập ngay";
                return RedirectToAction("DangNhap");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
                return View("DangKy");
            }
        }
        // Đăng nhập người dùng
        public ActionResult DangNhap()
        {
            return View();

        }
        // Xử lý đăng nhập người dùng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(FormCollection userlog)
        {
            ModelDienThoai dt = new ModelDienThoai();
            string userMail = userlog["userMail"];
            string password = userlog["password"];

            // Tìm kiếm trong bảng người dùng (NGUOI_DUNG)
            var user = dt.NGUOI_DUNG.SingleOrDefault(x => x.EMAIL == userMail);

            if (user != null)
            {
                // Kiểm tra mật khẩu của người dùng
                if (PasswordHelper.VerifyPassword(password, user.MATKHAU))
                {
                    Session["use"] = user;
                    // Nếu có quyền Admin (theo phân quyền) trong NGUOI_DUNG
                    var adminRole = dt.PHAN_QUYEN.FirstOrDefault(r => r.Ten_Quyen == "Admin")?.ID_Quyen;
                    if (user.ID_Quyen == adminRole)
                    {
                        return RedirectToAction("Index", "Admin/HomeAdmin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                // Nếu không tìm thấy trong NGUOI_DUNG, tìm kiếm trong bảng ADMIN
                var admin = dt.ADMIN.SingleOrDefault(a => a.EMAIL == userMail);
                if (admin != null && PasswordHelper.VerifyPassword(password, admin.MATKHAU))
                {
                    // Có thể gán session riêng cho admin, hoặc chuyển đổi sang kiểu chung
                    Session["admin"] = admin;
                    return RedirectToAction("Index", "Admin/HomeAdmin");
                }
            }

            TempData["Fail"] = "Tài khoản hoặc mật khẩu không chính xác.";
            return RedirectToAction("DangNhap");
        }


        public ActionResult DangXuat()
        {
            Session["use"] = null;
            Session["GioHang"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult HoSoNguoiDung(int? id)
        {
            ModelDienThoai dt = new ModelDienThoai();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUOI_DUNG nguoiDung = dt.NGUOI_DUNG.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }

            return View(nguoiDung);
        }

        public ActionResult Edit(int? id)
        {

            ModelDienThoai dt = new ModelDienThoai();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUOI_DUNG nguoidung = dt.NGUOI_DUNG.Find(id);
            if (nguoidung == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Quyen = dt.PHAN_QUYEN.ToList();
            return View(nguoidung);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NGUOI_DUNG nguoidung)
        {
            ModelDienThoai dt = new ModelDienThoai();
            ViewBag.ID_Quyen = dt.PHAN_QUYEN.ToList();

            if (ModelState.IsValid)
            {

                dt.Entry(nguoidung).State = EntityState.Modified;
                dt.SaveChanges();

                return RedirectToAction("HoSoNguoiDung", new { id = nguoidung.MA_ND });
            }

            return View(nguoidung);
        }
        public static byte[] encryptData(string data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedtytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedtytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedtytes;
        }
        public static string md5(string data)
        {
            return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();
        }
    }
}