using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiService2018.Database;
using TaxiService2018.Models;
using TaxiService2018.ViewModels;

namespace TaxiService2018.Controllers
{
    public class UserProfileController : Controller
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

            var appUser = db.ApplicationUsers.SingleOrDefault(u => u.Id == user.Id);
            if (appUser == null)
            {
                return HttpNotFound();
            }

            var editForm = new UserProfileEditForm(appUser);

            return View(editForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfileEditForm form)
        {
            var user = (ApplicationUser)Session["User"];
            if (user == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            if(!ModelState.IsValid)
            {
                return View("Edit", form);
            }

            var appUser = db.ApplicationUsers.SingleOrDefault(u => u.Id == user.Id);
            if (appUser == null)
            {
                return HttpNotFound();
            }

            appUser.Update(form);
            var userUpdate = new ApplicationUser();
            userUpdate = appUser;
            //maybe get login data from user?
            Session["User"] = userUpdate;
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