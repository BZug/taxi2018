using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiService2018.Database;
using TaxiService2018.Models;
using TaxiService2018.ViewModels;
using static TaxiService2018.Models.Enums;

namespace TaxiService2018.Controllers
{
    public class RideController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        public ActionResult Index()
        {
            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var user = (ApplicationUser)Session["User"];
            if (user == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            if (user.Role != UserRole.Dispatcher)
            {
                return new HttpUnauthorizedResult();
            }

            var drivers = db.ApplicationUsers.Where(u => u.Role == UserRole.Driver && !u.IsDriverBusy.Value).ToList();
            var driverList = drivers.Select(d => new SelectListItem { Text = $"{d.FirstName} {d.LastName}", Value = d.Id.ToString() });
            ViewBag.DriversList = new SelectList(driverList, "Value", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RideCreateForm form)
        {
            var user = (ApplicationUser)Session["User"];
            if (user == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            if (user.Role != UserRole.Dispatcher)
            {
                return new HttpUnauthorizedResult();
            }

            if (!ModelState.IsValid)
            {
                return View("Create", form);
            }

            var dbUser = db.ApplicationUsers.SingleOrDefault(u => u.Id == user.Id);
            if (dbUser == null)
            {
                return HttpNotFound();
            }

            var location = new Location(form);
            var driver = db.ApplicationUsers.SingleOrDefault(u => u.Id == form.DriverId);
            var ride = new Ride(location, dbUser, form.VehicleType, driver);
            driver.IsDriverBusy = true;
            db.Rides.Add(ride);
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