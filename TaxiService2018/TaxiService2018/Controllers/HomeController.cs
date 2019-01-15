using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using TaxiService2018.Database;
using TaxiService2018.Models;
using static TaxiService2018.Models.Enums;
using System.Data.Entity;
using TaxiService2018.ViewModels;
using System.IO;
using System.Web.Hosting;

namespace TaxiService2018.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        public ActionResult Index()
        {
            if(db.ApplicationUsers.ToList().Count == 0)
            {
                var dispatchers = getDispatchers("~/App_Data/Dispatchers.txt");
                foreach(var d in dispatchers)
                {
                    db.ApplicationUsers.Add(new ApplicationUser(d));
                }
                db.SaveChanges();
            }
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

            var rideTableRows = GenerateQuery(rides).ToList().Select(r => new RideTableSingleRow(r));

            return View("Home", rideTableRows);
        }

        [HttpGet]
        public ActionResult Search()
        {
            var user = (ApplicationUser)Session["User"];
            if (user == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            return View();
        }
        
        #region query
        private IQueryable<Ride> GenerateQuery(IQueryable<Ride> rides)
        {
            if (Request.QueryString["status"] != null)
            {
                var status = Request.QueryString["status"];

                switch (status)
                {
                    case "1":
                        rides = rides.Where(r => r.Status == RideStatus.Formed);
                        break;
                    case "2":
                        rides = rides.Where(r => r.Status == RideStatus.Failed);
                        break;
                    case "3":
                        rides = rides.Where(r => r.Status == RideStatus.Successful);
                        break;
                    case "0":
                    default:
                        break;
                }
            }

            if (Request.QueryString["sortBy"] != null)
            {
                var sortBy = Request.QueryString["sortBy"];

                switch (sortBy)
                {
                    case "1":
                        rides = rides.OrderByDescending(r => r.OrderDateTime);
                        break;
                    case "2":
                        rides = rides.OrderByDescending(r => r.Comment.Rating);
                        break;
                    case "0":
                    default:
                        break;
                }
            }

            if (Request.QueryString["orderDateMin"] != null)
            {
                var orderDateMin = Request.QueryString["orderDateMin"];

                if (DateTime.TryParse(orderDateMin, out DateTime result))
                {
                    rides = rides.Where(r => r.OrderDateTime >= result);
                }
            }

            if (Request.QueryString["orderDateMax"] != null)
            {
                var orderDateMax = Request.QueryString["orderDateMax"];

                if (DateTime.TryParse(orderDateMax, out DateTime result))
                {
                    rides = rides.Where(r => r.OrderDateTime <= result);
                }
            }

            if (Request.QueryString["ratingMin"] != null)
            {
                var ratingMin = Request.QueryString["ratingMin"];

                if (int.TryParse(ratingMin, out int result) && result > 0)
                {
                    rides = rides.Where(r => r.Status == RideStatus.Failed && (int)r.Comment.Rating >= result);
                }
            }

            if (Request.QueryString["ratingMax"] != null)
            {
                var ratingMax = Request.QueryString["ratingMax"];

                if (int.TryParse(ratingMax, out int result) && result > 0)
                {
                    rides = rides.Where(r => r.Status == RideStatus.Failed && (int)r.Comment.Rating <= result);
                }
            }

            if (Request.QueryString["priceMin"] != null)
            {
                var priceMin = Request.QueryString["priceMin"];

                if (int.TryParse(priceMin, out int result))
                {
                    rides = rides.Where(r => r.Status == RideStatus.Successful && r.Price.Value >= result);
                }
            }

            if (Request.QueryString["priceMax"] != null)
            {
                var priceMax = Request.QueryString["priceMax"];

                if (int.TryParse(priceMax, out int result))
                {
                    rides = rides.Where(r => r.Status == RideStatus.Successful && r.Price.Value <= result);
                }
            }

            if (Request.QueryString["firstName"] != null)
            {
                var firstName = Request.QueryString["firstName"];

                if (!string.IsNullOrWhiteSpace(firstName))
                {
                    rides = rides.Where(r => r.Driver.FirstName.ToLower().Contains(firstName.ToLower()));
                }
            }

            if (Request.QueryString["lastName"] != null)
            {
                var lastName = Request.QueryString["lastName"];

                if (!string.IsNullOrWhiteSpace(lastName))
                {
                    rides = rides.Where(r => r.Driver.LastName.ToLower().Contains(lastName.ToLower()));
                }
            }

            return rides;
        }
        #endregion query


        private List<string> getDispatchers(string path)
        {
            List<string> lines = new List<string>();


            FileStream fileStream = new FileStream(HostingEnvironment.MapPath(path), FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            string line = "";
            while ((line = streamReader.ReadLine()) != null)
            {
                lines.Add(line);
            }
            streamReader.Close();
            fileStream.Close();

            return lines;
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