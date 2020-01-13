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
        [Authorize(Roles = "Worker")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Materials/Create

        [Authorize(Roles = "Worker")]
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
        [Authorize(Roles = "Worker")]
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

        [Authorize(Roles = "Worker")]
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
        [Authorize(Roles = "Worker")]
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
        [Authorize(Roles = "Worker")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Materials materials = db.Materials.Find(id);
            db.Materials.Remove(materials);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
