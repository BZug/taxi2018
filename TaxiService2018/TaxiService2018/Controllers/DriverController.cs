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
    public class DriverController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DriverCreate()
        {
            var user = (ApplicationUser)Session["User"];
            if (user == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            if (user.Role != Enums.UserRole.Dispatcher)
            {
                return new HttpUnauthorizedResult();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DriverCreate (CreateDriverForm form)
        {
            var user = (ApplicationUser)Session["User"];
            if (user == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            if (user.Role != Enums.UserRole.Dispatcher)
            {
                return new HttpUnauthorizedResult();
            }

            if(!ModelState.IsValid)
            {
                return View("DriverCreate", form);
            }

            if(db.ApplicationUsers.Any(u => u.Username == form.Username))
            {
                ModelState.AddModelError("", "Selected username already exists.");
                return View("DriverCreate", form);
            }

            var driver = new ApplicationUser(form);
            db.ApplicationUsers.Add(driver);
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