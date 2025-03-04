using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SHOP_DIENTHOAI.Models;

namespace SHOP_DIENTHOAI.Areas.Admin.Controllers
{
    public class PhanQuyenController : Controller
    {
        ModelDienThoai dt = new ModelDienThoai();
        public ActionResult Index()
        {
            return View(dt.PHAN_QUYEN.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PHAN_QUYEN phanQuyen)
        {
            if (ModelState.IsValid)
            {
                dt.PHAN_QUYEN.Add(phanQuyen);
                dt.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phanQuyen);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHAN_QUYEN phanQuyen = dt.PHAN_QUYEN.Find(id);
            if (phanQuyen == null)
            {
                return HttpNotFound();
            }
            return View(phanQuyen);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PHAN_QUYEN phanQuyen)
        {
            if (ModelState.IsValid)
            {
                dt.Entry(phanQuyen).State = EntityState.Modified;
                dt.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phanQuyen);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHAN_QUYEN phanQuyen = dt.PHAN_QUYEN.Find(id);
            if (phanQuyen == null)
            {
                return HttpNotFound();
            }
            return View(phanQuyen);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHAN_QUYEN phanQuyen = dt.PHAN_QUYEN.Find(id);
            dt.PHAN_QUYEN.Remove(phanQuyen);
            dt.SaveChanges();
            return RedirectToAction("Index");
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
