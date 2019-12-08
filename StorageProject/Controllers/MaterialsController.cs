using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StorageProject.Models;

namespace StorageProject.Controllers
{
    public class MaterialsController : Controller
    {
        private StorageEntities4 db = new StorageEntities4();

        // GET: Materials
        public ActionResult Index()
        {
            return View(db.Materials.ToList());
        }

        // GET: Materials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Materials materials = db.Materials.Find(id);
            if (materials == null)
            {
                return HttpNotFound();
            }
            return View(materials);
        }

        // GET: Materials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Materials/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Quantity")] Materials materials)
        {
            if (ModelState.IsValid)
            {
                db.Materials.Add(materials);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(materials);
        }

        // GET: Materials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Materials materials = db.Materials.Find(id);
            if (materials == null)
            {
                return HttpNotFound();
            }
            return View(materials);
        }

        // POST: Materials/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Quantity")] Materials materials)
        {
            if (ModelState.IsValid)
            {
                db.Entry(materials).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(materials);
        }

        // GET: Materials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Materials materials = db.Materials.Find(id);
            if (materials == null)
            {
                return HttpNotFound();
            }
            return View(materials);
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Materials materials = db.Materials.Find(id);
            db.Materials.Remove(materials);
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
