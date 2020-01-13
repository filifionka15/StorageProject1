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
    [Authorize(Roles = "Superior")]
    public class TechnicsController : Controller
    {   
        private StorageEntities4 db = new StorageEntities4();

        // GET: Technics
        public ActionResult Index()
        {
            return View(db.Technics.ToList());
        }

        // GET: Technics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Technics technics = db.Technics.Find(id);
            if (technics == null)
            {
                return HttpNotFound();
            }
            return View(technics);
        }

        // GET: Technics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Technics/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Technics technics)
        {
            if (ModelState.IsValid)
            {
                db.Technics.Add(technics);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(technics);
        }

        // GET: Technics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Technics technics = db.Technics.Find(id);
            if (technics == null)
            {
                return HttpNotFound();
            }
            return View(technics);
        }

        // POST: Technics/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Technics technics)
        {
            if (ModelState.IsValid)
            {
                db.Entry(technics).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(technics);
        }

        // GET: Technics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Technics technics = db.Technics.Find(id);
            if (technics == null)
            {
                return HttpNotFound();
            }
            return View(technics);
        }

        // POST: Technics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Technics technics = db.Technics.Find(id);
            db.Technics.Remove(technics);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
