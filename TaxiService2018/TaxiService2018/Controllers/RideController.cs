using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiService2018.Database;
using TaxiService2018.Models;
using TaxiService2018.ViewModels;
using static TaxiService2018.Models.Enums;
using System.Data.Entity;

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
            if(drivers.Count<1)
            {
                return RedirectToAction("Home", "Home");
            }
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
            if(driver == null)
            {
                return HttpNotFound();
            }
            var ride = new Ride(location, dbUser, form.VehicleType, driver);
            driver.IsDriverBusy = true;
            db.Rides.Add(ride);
            db.SaveChanges();

            return RedirectToAction("Home", "Home");
        }

        [HttpGet]
        public ActionResult Active()
        {
            var user = (ApplicationUser)Session["User"];
            if (user == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            if (user.Role != UserRole.Driver)
            {
                return new HttpUnauthorizedResult();
            }

            var appRide = db.Rides.Include(r => r.Source).Include(r => r.Dispatcher).FirstOrDefault(r => r.Driver.Id == user.Id && r.Status == RideStatus.Formed);
            var activeRF = new ActiveRideForm(appRide);

            return View(activeRF);
        }

        [HttpGet]
        public ActionResult Successful(int id)
        {
            var user = (ApplicationUser)Session["User"];
            if (user == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            if (user.Role != UserRole.Driver)
            {
                return new HttpUnauthorizedResult();
            }

            var successfulF = new SuccesfulRideForm(id);

            return View(successfulF);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Successful(SuccesfulRideForm form)
        {
            var user = (ApplicationUser)Session["User"];
            if (user == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            if (user.Role != UserRole.Driver)
            {
                return new HttpUnauthorizedResult();
            }

            if(!ModelState.IsValid)
            {
                return View("Successful", form);
            }

            var ride = db.Rides.Include(r => r.Driver).SingleOrDefault(r => r.Id == form.IdR);
            var driver = db.ApplicationUsers.SingleOrDefault(u =>u.Id == ride.Driver.Id);
            var loc = new Location(form);
            ride.Update(form);
            driver.IsDriverBusy = false;
            Session["User"] = driver;
            db.SaveChanges();

            return RedirectToAction("Home", "Home");
        }

        [HttpGet]
        public ActionResult Failed(int id)
        {
            var user = (ApplicationUser)Session["User"];
            if (user == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            if (user.Role != UserRole.Driver)
            {
                return new HttpUnauthorizedResult();
            }

            var failedRide = new FailedRideForm(id);

            return View(failedRide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Failed(FailedRideForm form)
        {
            var user = (ApplicationUser)Session["User"];
            if (user == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            if (user.Role != UserRole.Driver)
            {
                return new HttpUnauthorizedResult();
            }

            if(!ModelState.IsValid)
            {
                return View("Failed", form);
            }

            var ride = db.Rides.Include(r => r.Driver).SingleOrDefault(r => r.Id == form.IdR);
            var driver = db.ApplicationUsers.SingleOrDefault(u => u.Id == ride.Driver.Id);
            ride.Update(form);
            driver.IsDriverBusy = false;
            Session["User"] = driver;
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