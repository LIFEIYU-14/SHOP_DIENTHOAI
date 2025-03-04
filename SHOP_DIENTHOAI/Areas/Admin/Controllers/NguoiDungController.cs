using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SHOP_DIENTHOAI.Models;
using BCrypt.Net;

namespace SHOP_DIENTHOAI.Areas.Admin.Controllers
{
    public class NguoiDungController : Controller
    {
        private readonly ModelDienThoai _context = new ModelDienThoai();

        // GET: Admin/NguoiDung
        public ActionResult Index()
        {
            return View(_context.NGUOI_DUNG.Include(u => u.PHAN_QUYEN).ToList());
        }
        // GET: Admin/NguoiDung/Create
        public ActionResult Create()
        {
            ViewBag.ID_Quyen = new SelectList(_context.PHAN_QUYEN, "ID_Quyen", "Ten_Quyen");
            return View();
        }
        // POST: Admin/NguoiDung/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NGUOI_DUNG nguoidung)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ID_Quyen = new SelectList(_context.PHAN_QUYEN, "ID_Quyen", "Ten_Quyen", nguoidung.ID_Quyen);
                return View(nguoidung);
            }

            // Kiểm tra mật khẩu có đủ dài không
            if (string.IsNullOrEmpty(nguoidung.MATKHAU) || nguoidung.MATKHAU.Length < 8)
            {
                ModelState.AddModelError("MATKHAU", "Mật khẩu phải có ít nhất 8 ký tự.");
                ViewBag.ID_Quyen = new SelectList(_context.PHAN_QUYEN, "ID_Quyen", "Ten_Quyen", nguoidung.ID_Quyen);
                return View(nguoidung);
            }

            // Mã hóa mật khẩu trước khi lưu vào database
            nguoidung.MATKHAU = PasswordHelper.HashPassword(nguoidung.MATKHAU);

            _context.NGUOI_DUNG.Add(nguoidung);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Admin/NguoiDung/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var nguoidung = _context.NGUOI_DUNG.Find(id);
            if (nguoidung == null) return HttpNotFound();

            return View(nguoidung);
        }

        // GET: Admin/NguoiDung/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var nguoidung = _context.NGUOI_DUNG.Find(id);
            if (nguoidung == null) return HttpNotFound();

            ViewBag.ID_Quyen = new SelectList(_context.PHAN_QUYEN, "ID_Quyen", "Ten_Quyen", nguoidung.ID_Quyen);
            return View(nguoidung);
        }

        // POST: Admin/NguoiDung/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NGUOI_DUNG nguoidung)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ID_Quyen = new SelectList(_context.PHAN_QUYEN, "ID_Quyen", "Ten_Quyen", nguoidung.ID_Quyen);
                return View(nguoidung);
            }

            var existingUser = _context.NGUOI_DUNG.Find(nguoidung.MA_ND);
            if (existingUser == null) return HttpNotFound();

            // Kiểm tra nếu ID_Quyen bị bỏ trống hoặc null
            if (nguoidung.ID_Quyen == null || nguoidung.ID_Quyen == 0)
            {
                ModelState.AddModelError("ID_Quyen", "Vui lòng chọn quyền người dùng.");
                ViewBag.ID_Quyen = new SelectList(_context.PHAN_QUYEN, "ID_Quyen", "Ten_Quyen", nguoidung.ID_Quyen);
                return View(nguoidung);
            }

            // Cập nhật thông tin
            existingUser.TEN_ND = nguoidung.TEN_ND;
            existingUser.EMAIL = nguoidung.EMAIL;
            existingUser.DIEN_THOAI = nguoidung.DIEN_THOAI;
            existingUser.DIA_CHI = nguoidung.DIA_CHI;
            existingUser.ID_Quyen = nguoidung.ID_Quyen;

            // Chỉ cập nhật mật khẩu nếu có thay đổi
            if (!string.IsNullOrEmpty(nguoidung.MATKHAU))
            {
                if (nguoidung.MATKHAU.Length < 8)
                {
                    ModelState.AddModelError("MATKHAU", "Mật khẩu phải có ít nhất 8 ký tự.");
                    ViewBag.ID_Quyen = new SelectList(_context.PHAN_QUYEN, "ID_Quyen", "Ten_Quyen", nguoidung.ID_Quyen);
                    return View(nguoidung);
                }
                existingUser.MATKHAU = PasswordHelper.HashPassword(nguoidung.MATKHAU);
            }

            _context.Entry(existingUser).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = nguoidung.MA_ND });
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }

            ViewBag.ID_Quyen = new SelectList(_context.PHAN_QUYEN, "ID_Quyen", "Ten_Quyen", nguoidung.ID_Quyen);
            return View(nguoidung);
        }

        // GET: Admin/NguoiDung/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var nguoidung = _context.NGUOI_DUNG.Find(id);
            if (nguoidung == null) return HttpNotFound();

            return View(nguoidung);
        }

        // POST: Admin/NguoiDung/DeleteConfirmed
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var nguoidung = _context.NGUOI_DUNG.Find(id);
            if (nguoidung == null)
            {
                return Json(new { success = false, message = "Người dùng không tồn tại." });
            }

            try
            {
                _context.NGUOI_DUNG.Remove(nguoidung);
                _context.SaveChanges();
                return Json(new { success = true, message = "Xóa người dùng thành công." });
            }
            catch
            {
                return Json(new { success = false, message = "Không thể xóa người dùng này vì có đơn hàng liên quan!" });
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing) _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
