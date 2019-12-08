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
    public class UsedMaterialsController : Controller
    {
        private StorageEntities4 db = new StorageEntities4();

        // GET: UsedMaterials
        public ActionResult Index()
        {
            var usedMaterial = db.UsedMaterial.Include(u => u.Materials).Include(u => u.Technics);
            return View(usedMaterial.ToList());
        }

        // GET: UsedMaterials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsedMaterial usedMaterial = db.UsedMaterial.Find(id);
            if (usedMaterial == null)
            {
                return HttpNotFound();
            }
            return View(usedMaterial);
        }

        // GET: UsedMaterials/Create
        public ActionResult Create()
        {
            ViewBag.Material = new SelectList(db.Materials, "Id", "Name");
            ViewBag.TechnicsUnit = new SelectList(db.Technics, "Id", "Name");
            return View();
        }

        // POST: UsedMaterials/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Material,TechnicsUnit,DateOfUse,QuantityOfMaterial")] UsedMaterial usedMaterial)
        {
            if (ModelState.IsValid)
            {
                Materials materials = db.Materials.Find(usedMaterial.Material);
                if (usedMaterial.QuantityOfMaterial <= materials.Quantity) {
                    db.UsedMaterial.Add(usedMaterial);
                    //db.SaveChanges();

                    materials.Quantity = materials.Quantity - usedMaterial.QuantityOfMaterial;
                    db.Entry(materials).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else { RedirectToAction("Index"); }

                return RedirectToAction("Index");
            }

            ViewBag.Material = new SelectList(db.Materials, "Id", "Name", usedMaterial.Material);
            ViewBag.TechnicsUnit = new SelectList(db.Technics, "Id", "Name", usedMaterial.TechnicsUnit);
            return View(usedMaterial);
        }



        // GET: UsedMaterials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsedMaterial usedMaterial = db.UsedMaterial.Find(id);
            if (usedMaterial == null)
            {
                return HttpNotFound();
            }
            return View(usedMaterial);
        }

        // POST: UsedMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsedMaterial usedMaterial = db.UsedMaterial.Find(id);
            Materials materials = db.Materials.Find(usedMaterial.Material);
            materials.Quantity = materials.Quantity + usedMaterial.QuantityOfMaterial;
            db.Entry(materials).State = EntityState.Modified;
            db.UsedMaterial.Remove(usedMaterial);
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
