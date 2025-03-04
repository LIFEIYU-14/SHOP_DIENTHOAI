using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SHOP_DIENTHOAI.Models;

namespace SHOP_DIENTHOAI.Areas.Admin.Controllers
{
    public class HangSanXuatAdminController : Controller
    {
        private ModelDienThoai db = new ModelDienThoai();


        public ActionResult Index()
        {
            return View(db.HANG_SAN_XUAT.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HANG_SAN_XUAT hsx = db.HANG_SAN_XUAT.Find(id);
            if (hsx == null)
            {
                return HttpNotFound();
            }
            return View(hsx);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HANG_SAN_XUAT hsx)
        {
            if (ModelState.IsValid)
            {
                db.HANG_SAN_XUAT.Add(hsx);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hsx);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HANG_SAN_XUAT hsx = db.HANG_SAN_XUAT.Find(id);
            if (hsx == null)
            {
                return HttpNotFound();
            }
            return View(hsx);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HANG_SAN_XUAT hsx)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hsx).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hsx);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HANG_SAN_XUAT hsx = db.HANG_SAN_XUAT.Find(id);
            if (hsx == null)
            {
                return HttpNotFound();
            }
            return View(hsx);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HANG_SAN_XUAT hsx = db.HANG_SAN_XUAT.Find(id);
            db.HANG_SAN_XUAT.Remove(hsx);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
