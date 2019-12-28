using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StorageProject.Models;

namespace StorageProject.Controllers
{
    [Authorize(Roles = "Worker")]
    public class AvgMaterialsController : Controller
    {
        private StorageEntities4 db = new StorageEntities4();
        // GET: Test
        public ActionResult Index(string dateFirst, string dateSecond)
        {
            
            
            Dictionary<string, int> avg = new Dictionary<string, int>();
            var usedMaterial = db.UsedMaterial.Include(u => u.Materials).Include(u => u.Technics);
            var materials = db.UsedMaterial.ToList();
            var mats = db.Materials.ToList();
            DateTime dateTimeFirst = new DateTime();
            DateTime dateTimeSecond = new DateTime();

            if (dateFirst != null & dateSecond != null)
            {
                ViewData["Message"] = dateFirst + " - " + dateSecond;
                try
                {
                    dateTimeFirst = DateTime.Parse(dateFirst);
                    dateTimeSecond = DateTime.Parse(dateSecond);
                }
                catch
                {
                    ViewData["Error"] = "Введите дату в формате ДД.ММ.ГГГГ";
                    ViewData["Message"] = "";
                }
                List<UsedMaterial> used = new List<UsedMaterial> { };
                foreach (var mat in materials)
                {
                    if (mat.DateOfUse >= dateTimeFirst & mat.DateOfUse <= dateTimeSecond)
                    {
                        used.Add(mat);
                    }
                }
                
                foreach (var u in used)
                {
                    if (avg.TryGetValue(u.Materials.Name, out int val))
                    {
                        avg[u.Materials.Name] += u.QuantityOfMaterial;
                    }
                    else
                        avg.Add(u.Materials.Name, u.QuantityOfMaterial);
                }
                return View(avg);
            }

            return View(avg);
        }

    }
}