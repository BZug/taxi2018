using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiService2018.Database;
using TaxiService2018.Models;
using static TaxiService2018.Models.Enums;
using System.Data.Entity;
using TaxiService2018.ViewModels;

namespace TaxiService2018.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        public ActionResult Index()
        {
            return RedirectToAction("Home");
        }

        [HttpGet]
        public ActionResult Home()
        {
            var user = (ApplicationUser)Session["User"];
            if (user == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            var rides = db.Rides.Include(r => r.Dispatcher)
                .Include(r => r.Driver)
                .Include(r => r.Source)
                .Include(r => r.Destination)
                .Include(r => r.Comment);

            

            if (user.Role == UserRole.Driver)
            {
                rides = rides.Where(r => r.Driver.Id == user.Id);
            }

            var rideTableRows = new List<RideTableSingleRow>();


            return View("Home", rideTableRows);
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