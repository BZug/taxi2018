using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiService2018.Database;
using TaxiService2018.Models;
using System.Data.Entity;
using TaxiService2018.ViewModels;

namespace TaxiService2018.Controllers
{
    public class LocationController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        public ActionResult Index()
        {
            return RedirectToAction("Edit");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var user = (ApplicationUser)Session["User"];
            if(user == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            if(user.Role != Enums.UserRole.Driver)
            {
                return new HttpUnauthorizedResult();
            }

            var appUser = db.ApplicationUsers.Include(u => u.Location).SingleOrDefault(u => u.Id == user.Id);
            if(appUser == null)
            {
                return HttpNotFound();
            }

            var locEditForm = new LocationEditForm(appUser.Location);

            return View(locEditForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LocationEditForm form)
        {
            var user = (ApplicationUser)Session["User"];
            if (user == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            if (user.Role != Enums.UserRole.Driver)
            {
                return new HttpUnauthorizedResult();
            }

            if(!ModelState.IsValid)
            {
                return View("Edit", form);
            }

            var appUser = db.ApplicationUsers.SingleOrDefault(u => u.Id == user.Id);

            if(appUser == null)
            {
                return HttpNotFound();
            }

            var loc = new Location(form);
            appUser.EditLocation(loc);
            Session["User"] = appUser;
            db.SaveChanges();

            return RedirectToAction("Home", "Home");
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