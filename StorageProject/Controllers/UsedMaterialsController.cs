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
        public ActionResult MaterialError()
        {
            return View();
        }
        public ActionResult QuantityError()
        {
            return View();
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
        [Authorize(Roles = "Worker, Superior")]
        public ActionResult Create()
        {
            ViewBag.Material = new SelectList(db.Materials, "Id", "Name");
            ViewBag.TechnicsUnit = new SelectList(db.Technics, "Id", "Name");
            return View();
        }

        // POST: UsedMaterials/Create
        [Authorize(Roles = "Worker, Superior")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Material,TechnicsUnit,DateOfUse,QuantityOfMaterial")] UsedMaterial usedMaterial)
        {
            if (ModelState.IsValid)
            {
                Materials materials = db.Materials.Find(usedMaterial.Material);
                if (usedMaterial.QuantityOfMaterial <= materials.Quantity & usedMaterial.QuantityOfMaterial>0)
                {
                    db.UsedMaterial.Add(usedMaterial);


                    materials.Quantity = materials.Quantity - usedMaterial.QuantityOfMaterial;
                    db.Entry(materials).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else if(usedMaterial.QuantityOfMaterial > materials.Quantity) { return RedirectToAction("MaterialError"); }
                else if (usedMaterial.QuantityOfMaterial <= 0) { return RedirectToAction("QuantityError"); }
                return RedirectToAction("Index");
            }

            ViewBag.Material = new SelectList(db.Materials, "Id", "Name", usedMaterial.Material);
            ViewBag.TechnicsUnit = new SelectList(db.Technics, "Id", "Name", usedMaterial.TechnicsUnit);
            return View(usedMaterial);
        }


        [Authorize(Roles = "Worker, Superior")]
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
        [Authorize(Roles = "Worker, Superior")]
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

    }
}

