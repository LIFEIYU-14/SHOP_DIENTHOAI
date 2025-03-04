using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHOP_DIENTHOAI.Models;
using System.IO;

namespace SHOP_DIENTHOAI.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        public ActionResult Index(string search = "", string sortBy = "Giá Bán tăng dần", int page = 1)
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
                    products = products.OrderByDescending(row => row.TEN_SP); // Sắp xếp giảm dần theo tên
                    break;
            }

            ViewBag.Search = search;
            ViewBag.SortBy = sortBy;
            int pageSize = 5; // Số sản phẩm trên mỗi trang
            int pageNumber = (page < 1) ? 1 : page; // Trang hiện tại

            // Sử dụng Skip và Take để phân trang sản phẩm
            var pagedProducts = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Tính tổng số trang
            int totalProducts = products.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;

            return View(pagedProducts);
        }
        public ActionResult ChiTiet(int id)
        {
            ModelDienThoai dt = new ModelDienThoai();
            var chitiet = dt.SAN_PHAM.Find(id);
            return View(chitiet);
        }

        public ActionResult ThemSP()
        {
            ModelDienThoai dt = new ModelDienThoai();

            ViewBag.HSX = dt.HANG_SAN_XUAT.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult ThemSP(SAN_PHAM sp, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                ModelDienThoai dt = new ModelDienThoai();
                ViewBag.HSX = dt.HANG_SAN_XUAT.ToList();

                // Kiểm tra sản phẩm đã tồn tại chưa
                var existingProduct = dt.SAN_PHAM.FirstOrDefault(p => p.TEN_SP == sp.TEN_SP && p.MA_HSX == sp.MA_HSX);

                if (existingProduct != null)
                {
                    // Nếu đã tồn tại, chỉ cộng dồn số lượng
                    existingProduct.SOLUONG += sp.SOLUONG;
                    dt.SaveChanges();
                    return RedirectToAction("Index");
                }

                // Nếu sản phẩm chưa tồn tại, thêm mới
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    if (imageFile.ContentLength > 5000000)
                    {
                        ModelState.AddModelError("Image", "Kích thước file không được lớn hơn 5MB.");
                        return View();
                    }

                    var allowExs = new[] { ".jpg" };
                    var fileEx = Path.GetExtension(imageFile.FileName).ToLower();
                    if (!allowExs.Contains(fileEx))
                    {
                        ModelState.AddModelError("Image", "Phần mở rộng file không hỗ trợ.");
                        return View();
                    }

                    sp.HinhAnh = "";
                    sp.Hinh1 = "";
                    sp.Hinh2 = "";
                    sp.Hinh3 = "";
                    sp.Hinh4 = "";

                    dt.SAN_PHAM.Add(sp);
                    dt.SaveChanges();

                    SAN_PHAM pro = dt.SAN_PHAM.OrderByDescending(p => p.MA_SP).First();

                    var fileName = pro.MA_SP.ToString() + fileEx;
                    var path = Path.Combine(Server.MapPath("~/Asset/images"), fileName);
                    imageFile.SaveAs(path);

                    pro.HinhAnh = fileName;
                    pro.Hinh1 = fileName;
                    pro.Hinh2 = fileName;
                    pro.Hinh3 = fileName;
                    pro.Hinh4 = fileName;

                    dt.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    sp.HinhAnh = "";
                    sp.Hinh1 = "";
                    sp.Hinh2 = "";
                    sp.Hinh3 = "";
                    sp.Hinh4 = "";
                    dt.SAN_PHAM.Add(sp);
                    dt.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            ModelDienThoai dt = new ModelDienThoai();
            ViewBag.HSX = dt.HANG_SAN_XUAT.ToList();
            var edit = dt.SAN_PHAM.Find(id);
            if (edit == null)
            {
                return HttpNotFound();
            }
            return View(edit);
        }
        [HttpPost]
        public ActionResult Edit(SAN_PHAM sanpham, HttpPostedFileBase imageFile)
        {
            ModelDienThoai dt = new ModelDienThoai();
            ViewBag.HSX = dt.HANG_SAN_XUAT.ToList();

            try
            {
                // Tìm sản phẩm cần chỉnh sửa theo MA_SP
                var oldItem = dt.SAN_PHAM.Find(sanpham.MA_SP);
                if (oldItem == null)
                {
                    return HttpNotFound();
                }

                // Cập nhật các trường thông tin
                oldItem.TEN_SP = sanpham.TEN_SP;
                // Cập nhật trường khóa ngoại thay vì gán đối tượng navigation
                oldItem.MA_HSX = sanpham.MA_HSX;
                oldItem.GIA = sanpham.GIA;
                oldItem.SOLUONG = sanpham.SOLUONG;
                oldItem.GHICHU_SP = sanpham.GHICHU_SP;

                // Xử lý file upload nếu có
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    if (imageFile.ContentLength > 5000000)
                    {
                        ModelState.AddModelError("Image", "Kích thước file không được lớn hơn 5MB.");
                        return View(sanpham);
                    }

                    var allowExs = new[] { ".jpg" };
                    var fileEx = Path.GetExtension(imageFile.FileName).ToLower();
                    if (!allowExs.Contains(fileEx))
                    {
                        ModelState.AddModelError("Image", "Phần mở rộng file không hỗ trợ.");
                        return View(sanpham);
                    }
                    var fileName = sanpham.MA_SP.ToString() + fileEx;
                    var path = Path.Combine(Server.MapPath("~/Asset/images"), fileName);
                    imageFile.SaveAs(path);

                    // Cập nhật các trường ảnh của sản phẩm
                    oldItem.HinhAnh = fileName;
                    oldItem.Hinh1 = fileName;
                    oldItem.Hinh2 = fileName;
                    oldItem.Hinh3 = fileName;
                    oldItem.Hinh4 = fileName;
                }

                dt.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Có thể thêm log cho ex.Message nếu cần
                ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
                return View(sanpham);
            }
        }
   
        // Xoá sản phẩm phương thức GET: Admin/Home/Delete/5
        public ActionResult Delete(int id)
        {
            ModelDienThoai dt = new ModelDienThoai();
            ViewBag.HSX = dt.HANG_SAN_XUAT.ToList();
            var delete = dt.SAN_PHAM.Find(id);
            return View(delete);
        }

        // Xoá sản phẩm phương thức POST: Admin/Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            ModelDienThoai dt = new ModelDienThoai();
            ViewBag.HSX = dt.HANG_SAN_XUAT.ToList();
            try
            {
                //Lấy được thông tin sản phẩm theo ID(mã sản phẩm)
                var delete = dt.SAN_PHAM.Find(id);
                // Xoá
                dt.SAN_PHAM.Remove(delete);
                // Lưu lại
                dt.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            
        }

    }
}
