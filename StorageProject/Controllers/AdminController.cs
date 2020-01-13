using StorageProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;

namespace StorageProject.Controllers
{   [Authorize(Roles ="Administrator")]
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var listUsers = db.Users.ToList();
            
            foreach (var user in listUsers)
            {
                if (user.Id == id)
                {
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    return View(user);
                }
            }
            return View(db.Users.ToList());
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            foreach (var user in db.Users.ToList())
            {
                if (user.Id == id)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult RoleWorker(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var listUsers = db.Users.ToList();

            foreach (var user in listUsers)
            {
                if (user.Id == id)
                {
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    return View(user);
                }
            }
            return View(db.Users.ToList());
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("RoleWorker")]
        [ValidateAntiForgeryToken]
        public ActionResult RoleWorkerConfirmed(string id)
        {
            foreach (var user in db.Users.ToList())
            {
                if (user.Id == id)
                {
                    ApplicationUserManager _app = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    IList<string> listRoles = _app.GetRoles(user.Id);
                    _app.RemoveFromRoles(user.Id, listRoles.ToArray());               
                    _app.AddToRole(user.Id, "Worker");
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            var userRoles = new List<RolesViewModel>();
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            foreach (var user in userStore.Users)
            {
                var r = new RolesViewModel
                {
                    UserName = user.UserName,
                    Id = user.Id
                };
                userRoles.Add(r);
            }
            foreach (var user in userRoles)
            {
                user.RoleNames = userManager.GetRoles(userStore.Users.First(s => s.UserName == user.UserName).Id);
            }

            return View(userRoles);
        }

        public ActionResult RoleSuperior(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listUsers = db.Users.ToList();

            foreach (var user in listUsers)
            {
                if (user.Id == id)
                {
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    return View(user);
                }
            }
            return View(db.Users.ToList());
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("RoleSuperior")]
        [ValidateAntiForgeryToken]
        public ActionResult RoleSuperiorConfirmed(string id)
        {

            foreach (var user in db.Users.ToList())
            {
                if (user.Id == id)
                {
                    ApplicationUserManager _app = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    IList<string> listRoles = _app.GetRoles(user.Id);
                    _app.RemoveFromRoles(user.Id, listRoles.ToArray());
                    _app.AddToRole(user.Id, "Superior");
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult RoleAdmin(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var listUsers = db.Users.ToList();

            foreach (var user in listUsers)
            {
                if (user.Id == id)
                {
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    return View(user);
                }
            }
            return View(db.Users.ToList());
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("RoleAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAdminConfirmed(string id)
        {     
            foreach (var user in db.Users.ToList())
            {
                if (user.Id == id)
                {
                    ApplicationUserManager _app = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    
                    IList<string> listRoles = _app.GetRoles(user.Id);
                    _app.RemoveFromRoles(user.Id, listRoles.ToArray());
                    _app.AddToRole(user.Id, "Administrator");
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }

}



